using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace CompactMode
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private readonly double _initialImageScale;
        private Animation _firstRowAnimation;
        private Animation _middleRowAnimation;
        private Animation _lastRowAnimation;
        private readonly double _initialFirstRowHeight;
        private readonly double _initialMiddleRowHeight;
        private readonly double _initialLastRowHeight;
        private readonly RowDefinition _firstRowDefinition;
        private readonly RowDefinition _middleRowDefinition;
        private readonly RowDefinition _lastRowDefinition;
        private bool _isScrolled;

        public MainPage()
        {
            InitializeComponent();

            _initialImageScale = MainImage.Scale;

            _firstRowDefinition = DashboardBarGrid.RowDefinitions[0];
            _initialFirstRowHeight = _firstRowDefinition.Height.Value;

            _middleRowDefinition = DashboardBarGrid.RowDefinitions[1];
            _initialMiddleRowHeight = _firstRowDefinition.Height.Value;

            _lastRowDefinition = DashboardBarGrid.RowDefinitions[2];
            _initialLastRowHeight = _lastRowDefinition.Height.Value;
        }

        private void ScrollView_OnScrolled(object sender, ScrolledEventArgs e)
        {
            var scrollView = (ScrollView)sender;

            // Move back to original height
            if (_isScrolled && (int)scrollView.ScrollY == 0
                          && _firstRowDefinition?.Height.Value < _initialFirstRowHeight
                          && _lastRowDefinition?.Height.Value < _initialLastRowHeight)
            {
                scrollView.IsEnabled = false;

                _firstRowAnimation = new Animation(
                    (d) => _firstRowDefinition.Height = new GridLength(d.Clamp(0, double.MaxValue)),
                    _firstRowDefinition.Height.Value, _initialFirstRowHeight, Easing.Linear, () => _firstRowAnimation = null);
                _firstRowAnimation.Commit(this, "first row animation", 16, 500);

                MainImage.ScaleTo(_initialImageScale, 500, Easing.Linear);

                _middleRowAnimation = new Animation(
                    (d) => _middleRowDefinition.Height = new GridLength(d.Clamp(90, double.MaxValue)),
                    _middleRowDefinition.Height.Value, _initialMiddleRowHeight, Easing.Linear, () => _middleRowAnimation = null);
                _middleRowAnimation.Commit(this, "middle row animation", 16, 500);


                _lastRowAnimation = new Animation(
                    (d) => _lastRowDefinition.Height = new GridLength(d.Clamp(0, double.MaxValue)),
                    _lastRowDefinition.Height.Value, _initialLastRowHeight, Easing.Linear, () => _lastRowAnimation = null);
                _lastRowAnimation.Commit(this, "last row animation", 16, 500, Easing.Linear, (v, c) =>
                {
                    _isScrolled = false;
                    scrollView.IsEnabled = true;
                });
            }
            // Hide the rows
            else if (!_isScrolled && _firstRowDefinition?.Height.Value >= _initialFirstRowHeight
                               && _lastRowDefinition?.Height.Value >= _initialLastRowHeight)
            {
                scrollView.IsEnabled = false;

                _firstRowAnimation = new Animation(
                    (d) => _firstRowDefinition.Height = new GridLength(d.Clamp(0, double.MaxValue)),
                    _initialFirstRowHeight, 0, Easing.Linear, () => _firstRowAnimation = null);
                _firstRowAnimation.Commit(this, "first row animation", 16, 500);


                MainImage.ScaleTo(_initialImageScale * 0.7, 500, Easing.Linear);

                _middleRowAnimation = new Animation(
                    (d) => _middleRowDefinition.Height = new GridLength(d.Clamp(65, double.MaxValue)),
                    _middleRowDefinition.Height.Value, _initialMiddleRowHeight, Easing.Linear, () => _middleRowAnimation = null);
                _middleRowAnimation.Commit(this, "middle row animation", 16, 500);


                _lastRowAnimation = new Animation(
                    (d) => _lastRowDefinition.Height = new GridLength(d.Clamp(0, double.MaxValue)),
                    _initialLastRowHeight, 0, Easing.Linear, () => _lastRowAnimation = null);
                _lastRowAnimation.Commit(this, "last row animation", 16, 500, Easing.Linear, (v, c) =>
                {
                    _isScrolled = true;
                    scrollView.IsEnabled = true;
                });
            }
        }
    }
}
