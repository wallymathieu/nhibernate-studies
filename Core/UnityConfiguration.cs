using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SomeBasicNHApp.Core;
using Microsoft.Practices.Unity;

namespace SomeBasicNHApp.Core
{
    public enum Runtime
    {
        Web=0,
        Console=1
    }
    public static class CustomUnityConfigurationExtensions
    {
        public static UnityContainer RegisterCore(this UnityContainer self, Runtime runtime)
        {
            switch (runtime)
            {
                case Runtime.Web:
                    self.RegisterType<IMapPath, WebMapPath>();
                    break;
                case Runtime.Console:
                    self.RegisterType<IMapPath, ConsoleMapPath>();
                    break;
            }
            return self;
        }
    }
}
