using DRS.Data;
using DRS.PostgreSQL;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Business
{
    public class AppCore
    {
        // 1. Create a new Simple Injector container
        static Container container = new Container();
        public static Container Container
        {
            get
            {
                return container;
            }
        }

        public static void Bootstrap(InjectionType type)
        {
            Lifestyle lifestyle = Lifestyle.Scoped;

            if (container.Options.DefaultScopedLifestyle == null)
            {
                switch (type)
                {
                    case InjectionType.NetCore:
                        lifestyle = Lifestyle.Singleton;
                        break;
                    case InjectionType.WebApi:
                        lifestyle = Lifestyle.Scoped;
                        break;
                    case InjectionType.WCF:
                        lifestyle = Lifestyle.Singleton;
                        break;
                    case InjectionType.WPF:
                        lifestyle = Lifestyle.Singleton;
                        break;
                    case InjectionType.WF:
                        lifestyle = Lifestyle.Singleton;
                        break;
                    default:
                        break;
                }

                //2.Configure the container (register)
                Container.Register<IUserDA, UserDA>(lifestyle);
                Container.Register<IDevolucionesDA, DevolucionesDA>(lifestyle);
                Container.Register<ILogDA, LogDA>(lifestyle);
                Container.Register<IEmailDA, EmailDA>(lifestyle);
                Container.Register<IMenuDA, MenuDA>(lifestyle);
                Container.Register<IPermissionDA, PermissionDA>(lifestyle);
                //Container.Register<UserBus>(lifestyle);

                // Reverting to the pre-v5 behavior
                container.Options.ResolveUnregisteredConcreteTypes = true;

                container.Verify();
            }
        }
    }

    public enum InjectionType
    {
        NetCore,
        WebApi,
        WCF,
        WPF,
        WF
    }
}
