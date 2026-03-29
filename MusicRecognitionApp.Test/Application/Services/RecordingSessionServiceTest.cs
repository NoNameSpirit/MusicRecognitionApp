using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MusicRecognitionApp.Application.Services.Implementations;
using MusicRecognitionApp.Application.Services.Interfaces;

namespace MusicRecognitionApp.Test.Application.Services
{
    public class RecordingSessionServiceTest
    {
        private readonly Mock<IServiceScopeFactory> _scopeFactoryMock;
        private readonly Mock<IServiceScope> _scopeMock;
        private readonly Mock<IServiceProvider> _providerMock;
        private readonly Mock<IRecorderService> _recorderServiceMock;
        private readonly RecordingSessionService _recordingSessionService;

        public RecordingSessionServiceTest()
        {
            _scopeFactoryMock = new Mock<IServiceScopeFactory>();
            _scopeMock = new Mock<IServiceScope>();
            _providerMock = new Mock<IServiceProvider>();
            _recorderServiceMock = new Mock<IRecorderService>();

            _scopeFactoryMock.Setup(f => f.CreateScope())
                .Returns(_scopeMock.Object);
            _scopeMock.Setup(s => s.ServiceProvider)
                .Returns(_providerMock.Object);
            _providerMock.Setup(p => p.GetService(typeof(IRecorderService)))
                .Returns(_recorderServiceMock.Object);

            _recordingSessionService = new RecordingSessionService(_scopeFactoryMock.Object);
        }

        #region StartRecordingAsync Tests

        [Fact]
        public async Task StartRecordingAsync_Success_ReturnsFilePath()
        {
            // Arrange
            string expectedFile = "path/to/recording.wav";
            _recorderServiceMock.Setup(r => r.RecordAudioFromMicrophoneAsync(15, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedFile);

            // Act
            var result = await _recordingSessionService.StartRecordingAsync();

            // Assert
            result.Should().Be(expectedFile);
            _recorderServiceMock.Verify(r => r.RecordAudioFromMicrophoneAsync(15, It.IsAny<CancellationToken>()), 
                Times.Once);
        }

        [Fact]
        public async Task StartRecordingAsync_AlreadyRecording_ThrowsInvalidOperationException()
        {
            // Arrange
            var tcs = new TaskCompletionSource<string>();
            _recorderServiceMock.Setup(r => r.RecordAudioFromMicrophoneAsync(15, It.IsAny<CancellationToken>()))
                .Returns(tcs.Task);

            var firstTask = _recordingSessionService.StartRecordingAsync();
            await Task.Delay(50);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _recordingSessionService.StartRecordingAsync());

            tcs.SetResult("file.wav");
            await firstTask;
        }

        [Fact]
        public async Task StartRecordingAsync_CanceledException_ThrowsException()
        {
            // Arrange
            _recorderServiceMock.Setup(r => r.RecordAudioFromMicrophoneAsync(15, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new OperationCanceledException());

            // Act & Assert
            await Assert.ThrowsAsync<OperationCanceledException>(async () 
                => await _recordingSessionService.StartRecordingAsync());
        }

        [Fact]
        public async Task StartRecordingAsync_GeneralError_ThrowsException()
        {
            // Arrange
            var exception = new Exception("Mic Error");
            _recorderServiceMock.Setup(r => r.RecordAudioFromMicrophoneAsync(15, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () 
                => await _recordingSessionService.StartRecordingAsync());
        }

        [Fact]
        public async Task StartRecordingAsync_CleansUpEventSubscription()
        {
            // Arrange
            string expectedFile = "path/to/recording.wav";
            _recorderServiceMock.Setup(r => r.RecordAudioFromMicrophoneAsync(15, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedFile);

            bool eventInvoked = false;
            _recordingSessionService.RecordingSession += (p) => eventInvoked = true;

            // Act
            await _recordingSessionService.StartRecordingAsync();
            _recorderServiceMock.Raise(r => r.RecordingProgress += null, 50);

            // Assert
            eventInvoked.Should().BeFalse();
        }

        #endregion

        [Fact]
        public void StopRecording_WithoutStart_DoesNotThrow()
        {
            // Act
            var ex = Record.Exception(() => _recordingSessionService.StopRecording());

            // Assert
            ex.Should().BeNull();
        }
    }
}