using MusicRecognitionApp.Services.UI.Interfaces;
using System.Drawing;
using Timer = System.Windows.Forms.Timer;

namespace MusicRecognitionApp.Services.UI
{
    public class AnimationService : IAnimationService
    {
        public void AddHoverAnimation(Control control)
        {
            var normalSize = new Size(300, 300);
            var normalLocation = new Point(150, 130);
            var hoverSize = new Size(310, 310);
            var hoverLocation = new Point(145, 125);

            var animTimer = new Timer() { Interval = 16 };
            var targetSize = normalSize;
            var targetLocation = normalLocation;

            animTimer.Tick += (s, e) =>
            {
                control.Size = new Size(
                    control.Width + (targetSize.Width - control.Width) / 4,
                    control.Height + (targetSize.Height - control.Height) / 4
                );

                control.Location = new Point(
                    control.Location.X + (targetLocation.X - control.Location.X) / 4,
                    control.Location.Y + (targetLocation.Y - control.Location.Y) / 4
                );

                if (Math.Abs(control.Width - targetSize.Width) < 2)
                    animTimer.Stop();
            };

            control.MouseEnter += (s, e) =>
            {
                targetSize = hoverSize;
                targetLocation = hoverLocation;
                animTimer.Start();
            };

            control.MouseLeave += (s, e) =>
            {
                targetSize = normalSize;
                targetLocation = normalLocation;
                animTimer.Start();
            };

            control.Cursor = Cursors.Hand;
        }
    }
}