using Toolbox.NFC.Reader.Card;

namespace Toolbox.NFC.Reader.Driver.Omnikey
{
    internal class OmnikeyDriver : DriverBase
    {
        public OmnikeyDriver(IntPtr hCard) : base(hCard)
        { }

        public static CardType GetCardType(int cardTypeValue)
        {
            return cardTypeValue switch
            {
                0x0001 or 0x0088 => CardType.Mifare_Standard_1k,
                0x0002 => CardType.Mifare_Standard_4k,
                0x0003 => CardType.Mifare_UltraLight,
                0x0006 => CardType.ST_MicroElectronics_SR176,
                0x0007 => CardType.ST_MicroElectronics_SRI4K_SRIX4K_SRIX512_SRI512_SRT512,
                0x000A => CardType.Atmel_AT88SC0808CRF,
                0x000B => CardType.Atmel_AT88SC1616CRF,
                0x000C => CardType.Atmel_AT88SC3216CRF,
                0x000D => CardType.Atmel_AT88SC6416CRF,
                0x0012 => CardType.Texas_Intruments_TAG_IT,
                0x0013 => CardType.ST_MicroElectronics_LRI512,
                0x0014 => CardType.ICODE_SLI,
                0x0016 => CardType.ICODE1,
                0x0021 => CardType.ST_MicroElectronics_LRI64,
                0x0024 => CardType.ST_MicroElectronics_LR12,
                0x0025 => CardType.ST_MicroElectronics_LRI128,
                0x0026 => CardType.Mifare_Mini,
                0x002F => CardType.Innovision_Jewel,
                0x0030 => CardType.Innovision_Topaz,
                0x0034 => CardType.Atmel_AT88RF04C,
                0x0035 => CardType.ICODE_SL2,
                0x003A => CardType.Mifare_UltraLight_C,
                0x003B => CardType.Desfire,
                0xFFA0 => CardType.Generic_unknown_14443_A_card,
                0xFFA1 => CardType.Kovio_RF_barcode,
                0xFFB0 => CardType.Generic_unknown_14443_B_card,
                0xFFB1 => CardType.ASK_CTS_256B,
                0xFFB2 => CardType.ASK_CTS_512B,
                0xFFB3 => CardType.Pre_standard_ST_MicroElectronics_SRI_4K,
                0xFFB4 => CardType.Pre_standard_ST_MicroElectronics_SRI_X512,
                0xFFB5 => CardType.Pre_standard_ST_MicroElectronics_SRI_512,
                0xFFB6 => CardType.Pre_standard_ST_MicroElectronics_SRT_512,
                0xFFB7 => CardType.Inside_Contactless_PICOTAG_PICOPASS,
                0xFFB8 => CardType.Generic_Atmel_AT88SC_AT88RF_card,
                0xFFC0 => CardType.Calypso_card_using_the_Innovatron_protocol,
                0xFFD0 => CardType.Generic_ISO_15693_from_unknown_manufacturer,
                0xFFD1 => CardType.Generic_ISO_15693_from_EMMarin_or_Legic,
                0xFFD2 => CardType.Generic_ISO_15693_from_ST_MicroElectronics_block_number_on_8_bits,
                0xFFD3 => CardType.Generic_ISO_15693_from_ST_MicroElectronics_block_number_on_16_bits,
                0xFFFF => CardType.Virtual_card,
                _ => CardType.Unknown_Card_Type,
            };
        }
    }
}
