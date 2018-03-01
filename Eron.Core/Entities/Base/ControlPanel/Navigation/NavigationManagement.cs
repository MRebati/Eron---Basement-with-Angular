namespace Eron.Core.Entities.Base.ControlPanel.Navigation
{
    public static class NavigationManagement
    {
        private static NavigationModel _model;

        static NavigationManagement()
        {
            _model = FetchAllNavigationModels();
        }

        #region Get

        public static NavigationModel GetDashboardNavigation()
        {
            return _model;
        }

        #endregion

        #region Set

        private static NavigationModel FetchAllNavigationModels()
        {
            return new NavigationModel();
        }

        #endregion
    }
}