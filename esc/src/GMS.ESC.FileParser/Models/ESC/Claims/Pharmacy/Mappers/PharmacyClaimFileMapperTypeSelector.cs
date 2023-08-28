using FlatFiles.TypeMapping;
using static GMS.ESC.FileParser.Models.ESC.Claims.Pharmacy.Mappers.ClaimRecordTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Claims.Pharmacy.Mappers.FileBatchControlTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Claims.Pharmacy.Mappers.FileHeaderTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Claims.Pharmacy.Mappers.PayeeAddressRecordTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Claims.Pharmacy.Mappers.PayeeBatchControlTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Claims.Pharmacy.Mappers.PharmacyBatchControlTypeMapper;
using static GMS.ESC.FileParser.Models.ESC.Claims.Pharmacy.Mappers.PharmacyHeaderTypeMapper;

namespace GMS.ESC.FileParser.Models.ESC.Claims.Pharmacy.Mappers
{
    public static class PharmacyFileMapperTypeSelector
    {
        public static FixedLengthTypeMapperSelector GetPharmacyClaimFileMapperTypeSelector()
        {
            var selector = new FixedLengthTypeMapperSelector();
            selector.When(values => values.StartsWith("0")).Use(GetFileHeaderTypeMapper());
            selector.When(values => values.StartsWith("2")).Use(GetPharmacyHeaderTypeMapper());
            selector.When(values => values.StartsWith("3")).Use(GetPayeeAddressRecordTypeMapper());
            selector.When(values => values.StartsWith("4") || values.StartsWith("5")).Use(GetClaimRecordTypeMapper());
            selector.When(values => values.StartsWith("6")).Use(GetPharmacyBatchControlTypeMapper());
            selector.When(values => values.StartsWith("7")).Use(GetPayeeBatchControlTypeMapper());
            selector.When(values => values.StartsWith("8")).Use(GetFileBatchControlTypeMapper());
            return selector;
        }
    }
}
