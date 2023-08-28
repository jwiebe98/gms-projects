using FlatFiles.TypeMapping;
using static GMS.ESC.FileParser.Models.ESC.Predetermination.Mappers.GeneralRecordTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Predetermination.Mappers.ClientAddressTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Predetermination.Mappers.PredetermineDetailRecordTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Predetermination.Mappers.FileHeaderTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Predetermination.Mappers.ProviderHeaderTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Predetermination.Mappers.ProviderBatchControlTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Predetermination.Mappers.ClientBatchControlTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Predetermination.Mappers.TapeBatchControlTypeMapper;


namespace GMS.ESC.FileParser.Models.ESC.Predetermination.Mappers
{
    public static class PredeterminationFileMapperTypeSelector
    {
        public static FixedLengthTypeMapperSelector GetPredeterminationFileMapperTypeSelector()
        {
            var selector = new FixedLengthTypeMapperSelector();
            selector.When(values => values.StartsWith("0")).Use(GetFileHeaderTypeMapper());
            selector.When(values => values.StartsWith("2")).Use(GetProviderHeaderTypeMapper());
            selector.When(values => values.StartsWith("3")).Use(GetClientAddressTypeMapper());
            selector.When(values => values.StartsWith("4")).Use(GetGeneralRecordTypeMapper());
            selector.When(values => values.StartsWith("5")).Use(GetPredetermineDetailRecordTypeMapper());
            selector.When(values => values.StartsWith("6")).Use(GetProviderBatchControlTypeMapper());
            selector.When(values => values.StartsWith("7")).Use(GetClientBatchControlTypeMapper());
            selector.When(values => values.StartsWith("8")).Use(GetTapeBatchControlTypeMapper());
            return selector;
        }
    }
}
