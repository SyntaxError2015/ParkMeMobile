using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.OS;
using Android.Util;

namespace ParkMeMobile.Android.UI.Activities
{
    [Activity(Label = "ParkMe!", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private MapFragment mMapFragment;
        private LocationManager mLocationManager;
        private string mLocationProvider;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            InitializeLocationManager();
            InitializeMaps();
        }

        private void InitializeMaps()
        {
            var location = new LatLng(50.897778, 3.013333);
            var builder = CameraPosition.InvokeBuilder();
            builder.Target(location);
            builder.Zoom(18);
            builder.Bearing(155);
            builder.Tilt(65);
            var cameraPosition = builder.Build();
            var cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);

            mMapFragment = (MapFragment) FragmentManager.FindFragmentById(Resource.Id.map_frag);
            var map = mMapFragment.Map;

            if (map == null)
            {
                return;
            }

            map.MapType = GoogleMap.MapTypeNormal;
            map.UiSettings.ZoomControlsEnabled = true;
            map.UiSettings.ZoomGesturesEnabled = true;
            map.UiSettings.CompassEnabled = true;
            map.UiSettings.MyLocationButtonEnabled = true;

            map.MoveCamera(cameraUpdate);
        }

        private void InitializeLocationManager()
        {
            mLocationManager = (LocationManager) GetSystemService(LocationService);

            var criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };

            var acceptableLocationProviders = mLocationManager.GetProviders(criteriaForLocationService, true);

            mLocationProvider = acceptableLocationProviders.Any() ? acceptableLocationProviders.First() : string.Empty;
        }
    }
}