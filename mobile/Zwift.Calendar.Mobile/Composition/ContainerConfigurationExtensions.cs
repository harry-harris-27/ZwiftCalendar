using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Text;

namespace Zwift.Calendar.Mobile.Composition
{
    public static class ContainerConfigurationExtensions
    {
        public static ContainerConfiguration WithExport<T>(this ContainerConfiguration configuration, T instance, string contractName = null, IDictionary<string, object> metadata = null)
        {
            return WithExport(configuration, instance, typeof(T), contractName, metadata);
        }

        public static ContainerConfiguration WithExport(this ContainerConfiguration configuration, object instance, Type contractType, string contractName = null, IDictionary<string, object> metadata = null)
        {
            return configuration.WithProvider(new InstanceExportDescriptorProvider(
                instance, contractType, contractName, metadata));
        }
    }
}
