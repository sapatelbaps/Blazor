﻿using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beam.Client
{
    public static class Javascript
    {
        public static Task LoadAnimation(string elementId, int width, int height)
        {
            return JSRuntime.Current.InvokeAsync<object>
                ("animatedBeam.loadAnimation", elementId, width, height);
        }
    }
}
