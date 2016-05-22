using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.OS;
using ParkMeMobile.Common.Models;
using ParkMeMobile.Common.Polling;

namespace ParkMeMobile.Android.UI.Activities
{
    [Activity(Label = "ParkMe!", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, ILocationListener
    {
        private const double LATITUDE = 45.753700; //45.7482311;
        private const double LONGITUDE = 21.198585; //21.2398982;

        private MapFragment mMapFragment;
        private GoogleMap mMap;

        private PollingService<IList<Park>> mPollingService;

        #region Lifecycle

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            mPollingService = new PollingService<IList<Park>>(UpdateUi);

            InitializeLocationManager();
            InitializeMaps();

            MoveCameraOnCurrentPosition();
        }

        protected override void OnResume()
        {
            base.OnResume();

            mPollingService.StartTimer();
        }

        protected override void OnPause()
        {
            base.OnPause();

            mPollingService.StopTimer();
        }

        #endregion

        #region UI updates

        private void UpdateUi(IList<Park> parks)
        {
            RunOnUiThread(() =>
            {
                mMap.Clear();

                foreach (var park in parks)
                {
                    var i = 0;
                    foreach (var slot in park.Slots)
                    {
                        if (slot.IsOccupied)
                        {
                            continue;
                        }

                        var marker = new MarkerOptions();
                        marker.SetPosition(new LatLng(slot.Position.X, slot.Position.Y));
                        marker.SetTitle($"Parking slot {i}");

                        mMap.AddMarker(marker);

                        i++;
                    }
                }
            });
        }

        #endregion

        #region Maps management

        private void InitializeMaps()
        {
            mMapFragment = (MapFragment) FragmentManager.FindFragmentById(Resource.Id.map_frag);
            mMap = mMapFragment.Map;

            if (mMap == null)
            {
                return;
            }

            mMap.MapType = GoogleMap.MapTypeNormal;
            mMap.UiSettings.ZoomControlsEnabled = true;
            mMap.UiSettings.ZoomGesturesEnabled = true;
            mMap.UiSettings.CompassEnabled = true;
            mMap.UiSettings.MyLocationButtonEnabled = true;
        }

        private void MoveCameraOnCurrentPosition()
        {
            var location = new LatLng(LATITUDE, LONGITUDE);

            var locationBuilder = CameraPosition.InvokeBuilder();
            locationBuilder.Target(location);
            locationBuilder.Zoom(17);

            var position = CameraUpdateFactory.NewCameraPosition(locationBuilder.Build());
            mMap.MoveCamera(position);
        }

        #endregion

        #region Location management

        private void InitializeLocationManager()
        {
        }

        public void OnLocationChanged(Location location)
        {
        }

        public void OnProviderDisabled(string provider)
        {
        }

        public void OnProviderEnabled(string provider)
        {
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
        }

        #endregion
    }
}