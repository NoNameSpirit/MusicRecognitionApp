using FluentAssertions;
using Moq;
using MusicRecognitionApp.Application.Services.Interfaces;
using MusicRecognitionApp.Controls;
using MusicRecognitionApp.Core.Enums;
using MusicRecognitionApp.Presentation.Services.Implementation;
using MusicRecognitionApp.Presentation.Services.Interfaces;

namespace MusicRecognitionApp.Test.Presentation.Services
{
    public class StateRegistryServiceTest
    {
        private readonly Mock<IMessageBoxService> _messageBoxMock;
        private readonly Mock<ICardService> _cardServiceMock;
        private readonly Mock<IAnimationService> _animationServiceMock;
        private readonly Mock<ISongAddingService> _songAddingServiceMock;
        private readonly Mock<IRecordingSessionService> _recordingSessionMock;
        private readonly Mock<IAnalyzingSessionService> _analyzingSessionMock;
        private readonly Mock<IProcessingAudio> _processingAudioMock;
        private readonly Mock<IResultDisplayService> _resultDisplayMock;

        private readonly StateRegistryService _registry;

        public StateRegistryServiceTest()
        {
            _messageBoxMock = new Mock<IMessageBoxService>();
            _cardServiceMock = new Mock<ICardService>();
            _animationServiceMock = new Mock<IAnimationService>();
            _songAddingServiceMock = new Mock<ISongAddingService>();
            _recordingSessionMock = new Mock<IRecordingSessionService>();
            _analyzingSessionMock = new Mock<IAnalyzingSessionService>();
            _processingAudioMock = new Mock<IProcessingAudio>();
            _resultDisplayMock = new Mock<IResultDisplayService>();

            _registry = new StateRegistryService(
                _messageBoxMock.Object,
                _cardServiceMock.Object,
                _animationServiceMock.Object,
                _songAddingServiceMock.Object,
                _recordingSessionMock.Object,
                _analyzingSessionMock.Object,
                _processingAudioMock.Object,
                _resultDisplayMock.Object);
        }

        [Fact]
        public void CreateStateControl_ReadyState_ReturnsReadyStateControl()
        {
            // Arrange
            var mockManager = new Mock<IStateManagerService>();

            // Act
            var control = _registry.CreateStateControl(AppState.Ready, mockManager.Object);

            // Assert
            control.Should().BeOfType<ReadyStateControl>();
        }

        [Fact]
        public void CreateStateControl_RecordingState_ReturnsRecordingStateControl()
        {
            // Arrange
            var mockManager = new Mock<IStateManagerService>();

            // Act
            var control = _registry.CreateStateControl(AppState.Recording, mockManager.Object);

            // Assert
            control.Should().BeOfType<RecordingStateControl>();
        }

        [Fact]
        public void CreateStateControl_AnalyzingState_ReturnsAnalyzingStateControl()
        {
            // Arrange
            var mockManager = new Mock<IStateManagerService>();

            // Act
            var control = _registry.CreateStateControl(AppState.Analyzing, mockManager.Object);

            // Assert
            control.Should().BeOfType<AnalyzingStateControl>();
        }

        [Fact]
        public void CreateStateControl_ResultState_ReturnsResultStateControl()
        {
            // Arrange
            var mockManager = new Mock<IStateManagerService>();

            // Act
            var control = _registry.CreateStateControl(AppState.Result, mockManager.Object);

            // Assert
            control.Should().BeOfType<ResultStateControl>();
        }

        [Fact]
        public void CreateStateControl_LibraryState_ReturnsLibraryStateControl()
        {
            // Arrange
            var mockManager = new Mock<IStateManagerService>();

            // Act
            var control = _registry.CreateStateControl(AppState.Library, mockManager.Object);

            // Assert
            control.Should().BeOfType<LibraryStateControl>();
        }

        [Fact]
        public void CreateStateControl_ProcessingState_ReturnsProcessingStateControl()
        {
            // Arrange
            var mockManager = new Mock<IStateManagerService>();

            // Act
            var control = _registry.CreateStateControl(AppState.Processing, mockManager.Object);

            // Assert
            control.Should().BeOfType<ProcessingStateControl>();
        }

        [Fact]
        public void CreateStateControl_UnknownState_ThrowsException()
        {
            // Arrange
            var mockManager = new Mock<IStateManagerService>();
            var invalidState = (AppState)999;

            // Act & Assert
            var act = () => _registry.CreateStateControl(invalidState, mockManager.Object);

            act.Should().Throw<Exception>().WithMessage("Don't have this factory*");
        }

        [Fact]
        public void GetStatesControls_ReturnsAllExpectedStates()
        {
            // Act
            var states = _registry.GetStatesControls().ToList();

            // Assert
            states.Should().HaveCount(6);
            states.Should().Contain(AppState.Ready);
            states.Should().Contain(AppState.Recording);
            states.Should().Contain(AppState.Analyzing);
            states.Should().Contain(AppState.Result);
            states.Should().Contain(AppState.Library);
            states.Should().Contain(AppState.Processing);
        }
    }
}