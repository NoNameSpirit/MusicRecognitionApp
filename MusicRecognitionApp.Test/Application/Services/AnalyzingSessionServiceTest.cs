using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MusicRecognitionApp.Application.Models;
using MusicRecognitionApp.Application.Services.Implementations;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Core.Models.Business;

namespace MusicRecognitionApp.Tests.Application.Services
{
    public class AnalyzingSessionServiceTests
    {
        private readonly Mock<IServiceScopeFactory> _ScopeFactoryMock;
        private readonly Mock<IServiceScope> _ScopeMock;
        private readonly Mock<IServiceProvider> _ServiceProviderMock;
        private readonly Mock<IRecognitionService> _RecognitionServiceMock;

        private readonly AnalyzingSessionService _analyzingSessionService;

        public AnalyzingSessionServiceTests()
        {
            _ScopeFactoryMock = new Mock<IServiceScopeFactory>();
            _ScopeMock = new Mock<IServiceScope>();
            _ServiceProviderMock = new Mock<IServiceProvider>();
            _RecognitionServiceMock = new Mock<IRecognitionService>();

            _ScopeFactoryMock.Setup(f => f.CreateScope())
                .Returns(_ScopeMock.Object);
            _ScopeMock.Setup(s => s.ServiceProvider)
                .Returns(_ServiceProviderMock.Object);
            _ServiceProviderMock.Setup(p => p.GetService(typeof(IRecognitionService)))
                .Returns(_RecognitionServiceMock.Object);

            _analyzingSessionService = new AnalyzingSessionService(_ScopeFactoryMock.Object);
        }

        #region StartAnalyzingAsync Tests

        [Fact]
        public async Task StartAnalyzingAsync_Success_ResultsAndDeletesFile()
        {
            // Arrange
            var tempFile = Path.GetTempFileName();
            var expectedResults = new List<SearchResult> 
            { 
                new SearchResult(new SongModel(), 1, 0.9) 
            };
            _RecognitionServiceMock.Setup(r => r.RecognizeFromMicrophoneAsync(tempFile, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResults);

            try
            {
                // Act
                var result = await _analyzingSessionService.StartAnalyzingAsync(tempFile);

                // Assert
                result.Should().BeEquivalentTo(expectedResults);
                File.Exists(tempFile).Should().BeFalse("File must be deleted after success");
                _RecognitionServiceMock.Verify(r => r.RecognizeFromMicrophoneAsync(tempFile, It.IsAny<CancellationToken>()), 
                    Times.Once);
            }
            finally
            {
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
            }
        }

        [Fact]
        public async Task StartAnalyzingAsync_CanceledException_ThrowsAndDeletesFile()
        {
            // Arrange
            var tempFile = Path.GetTempFileName();

            _RecognitionServiceMock.Setup(r => r.RecognizeFromMicrophoneAsync(tempFile, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new OperationCanceledException());

            try
            {
                // Act & Assert
                await Assert.ThrowsAsync<OperationCanceledException>(async () =>
                    await _analyzingSessionService.StartAnalyzingAsync(tempFile));

                File.Exists(tempFile).Should().BeFalse("File must be deleted when an canceled exception occurs");
            }
            finally
            {
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
            }
        }

        [Fact]
        public async Task StartAnalyzingAsync_GeneralError_ThrowsAndDeletesFile()
        {
            // Arrange
            var tempFile = Path.GetTempFileName();
            var exception = new Exception("Recognition Failed");

            _RecognitionServiceMock.Setup(r => r.RecognizeFromMicrophoneAsync(tempFile, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            try
            {
                // Act & Assert
                await Assert.ThrowsAsync<Exception>(async () => 
                    await _analyzingSessionService.StartAnalyzingAsync(tempFile));

                File.Exists(tempFile).Should().BeFalse("File must be deleted when an exception occurs");
            }
            finally
            {
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
            }
        }

        [Fact]
        public async Task StartAnalyzingAsync_ConnectionLost_CleansUpEventSubscription()
        {
            // Arrange
            var tempFile = Path.GetTempFileName();
            _RecognitionServiceMock.Setup(r => r.RecognizeFromMicrophoneAsync(tempFile, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<SearchResult>());

            bool eventInvokedAfterComplete = false;
            _analyzingSessionService.AnalyzingSession += (p) 
                => eventInvokedAfterComplete = true;

            try
            {
                // Act
                await _analyzingSessionService.StartAnalyzingAsync(tempFile);

                _RecognitionServiceMock.Raise(r => r.AnalysisProgress += null, 50);

                // Assert
                eventInvokedAfterComplete.Should().BeFalse("The event must be unsubscribed in finally block");
            }
            finally
            {
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
            }
        }

        #endregion

        [Fact]
        public void StopAnalyzing_WithoutStart_DoesNotThrow()
        {
            // Act
            var ex = Record.Exception(() => _analyzingSessionService.StopAnalyzing());

            // Assert
            ex.Should().BeNull("Calling Stop without starting should't cause NullReferenceException");
        }
    }
}