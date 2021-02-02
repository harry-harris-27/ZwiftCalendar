using System;
using System.Collections.Generic;
using System.Composition.Hosting.Core;
using System.Text;

namespace Zwift.Calendar.Mobile.Composition
{
    internal class InstanceExportDescriptorProvider : SinglePartExportDescriptorProvider
    {

        private object _exportedInstance;


        public InstanceExportDescriptorProvider(object exportedInstance, Type contractType, string contractName = null, IDictionary<string, object> metadata = null)
            : base(contractType, contractName, metadata)
        {
            _exportedInstance = exportedInstance ?? throw new ArgumentNullException(nameof(exportedInstance));
        }


        public override IEnumerable<ExportDescriptorPromise> GetExportDescriptors(CompositionContract contract, DependencyAccessor descriptorAccessor)
        {
            if (IsSupportedContract(contract))
            {
                yield return new ExportDescriptorPromise(contract, _exportedInstance.ToString(), true, NoDependencies, _ =>
                    ExportDescriptor.Create((c, o) => _exportedInstance, Metadata));
            }
        }
    }
}
