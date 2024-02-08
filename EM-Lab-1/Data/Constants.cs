﻿using System.IO;
using System.Windows.Media;

namespace EM_Lab_1
{
    public static class Constants
    {
        private static double? _normalDistributionQuantile;

        public const double Tolerance = 0.00000001;
        public const double Alpha = 0.05;
        public const double C0 = 2.515517;
        public const double C1 = 0.802853;
        public const double C2 = 0.010328;
        public const double D1 = 1.432788;
        public const double D2 = 0.1892659;
        public const double D3 = 0.001308;

        public static double NormalDistributionQuantile
        {
            get
            {
                if (_normalDistributionQuantile == null)
                    _normalDistributionQuantile = Compute.NormalDistributionQuantile(1 - Alpha / 2);

                return _normalDistributionQuantile!.Value;
            }
        }

        public static readonly Color OkColor = Color.FromRgb(161, 255, 162);
        public static readonly Color NotOkColor = Color.FromRgb(255, 164, 161);
        public static readonly Color ActiveColor = Color.FromRgb(204, 204, 204);
        public static readonly Color InactiveColor = Color.FromRgb(121, 121, 121);
    }
}
