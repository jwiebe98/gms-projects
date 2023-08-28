using FlatFiles.TypeMapping;
using static GMS.ESC.FileParser.Models.ESC.Claims.Health.Mappers.ClaimRecordTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Claims.Health.Mappers.ClientAddressTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Claims.Health.Mappers.ClientBatchControlTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Claims.Health.Mappers.FileBatchControlTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Claims.Health.Mappers.FileHeaderTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Claims.Health.Mappers.ProviderBatchControlTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Claims.Health.Mappers.ProviderHeaderTypeMapper;

namespace GMS.ESC.FileParser.Models.ESC.Claims.Health.Mappers
{
    public static class HealthClaimFileMapperTypeSelector
    {
        public static FixedLengthTypeMapperSelector GetHealthClaimFileMapperTypeSelector()
        {
            var selector = new FixedLengthTypeMapperSelector();
            selector.When(values => values.StartsWith("0")).Use(GetFileHeaderTypeMapper());
            selector.When(values => values.StartsWith("2")).Use(GetProviderHeaderTypeMapper());
            selector.When(values => values.StartsWith("3")).Use(GetClientAddressTypeMapper());
            selector.When(values => values.StartsWith("4") || values.StartsWith("5")).Use(GetClaimRecordTypeMapper());
            selector.When(values => values.StartsWith("6")).Use(GetProviderBatchControlTypeMapper());
            selector.When(values => values.StartsWith("7")).Use(GetClientBatchControlTypeMapper());
            selector.When(values => values.StartsWith("8")).Use(GetFileBatchControlTypeMapper());
            return selector;
        }
    }
}
