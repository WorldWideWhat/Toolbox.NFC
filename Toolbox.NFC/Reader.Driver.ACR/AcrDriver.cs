using System;
using Toolbox.NFC.Enums;

namespace Toolbox.NFC.Reader.Driver.ACR
{
    internal class AcrDriver : DriverBase
    {
        public AcrDriver(IntPtr hCard) : base(hCard)
        {
        }

        public static CardType GetCardType(int cardTypeValue)
        {
            switch(cardTypeValue)
            {
                case 0x0001:
                case 0x0088: return CardType.Mifare_Standard_1k;
                case 0x0002: return CardType.Mifare_Standard_4k;
                case 0x0003: return CardType.Mifare_UltraLight;
                case 0x0006: return CardType.ST_MicroElectronics_SR176;
                case 0x0007: return CardType.ST_MicroElectronics_SRI4K_SRIX4K_SRIX512_SRI512_SRT512;
                case 0x000A: return CardType.Atmel_AT88SC0808CRF;
                case 0x000B: return CardType.Atmel_AT88SC1616CRF;
                case 0x000C: return CardType.Atmel_AT88SC3216CRF;
                case 0x000D: return CardType.Atmel_AT88SC6416CRF;
                case 0x0012: return CardType.Texas_Intruments_TAG_IT;
                case 0x0013: return CardType.ST_MicroElectronics_LRI512;
                case 0x0014: return CardType.ICODE_SLI;
                case 0x0016: return CardType.ICODE1;
                case 0x0021: return CardType.ST_MicroElectronics_LRI64;
                case 0x0024: return CardType.ST_MicroElectronics_LR12;
                case 0x0025: return CardType.ST_MicroElectronics_LRI128;
                case 0x0026: return CardType.Mifare_Mini;
                case 0x002F: return CardType.Innovision_Jewel;
                case 0x0030: return CardType.Innovision_Topaz;
                case 0x0034: return CardType.Atmel_AT88RF04C;
                case 0x0035: return CardType.ICODE_SL2;
                case 0x003A: return CardType.Mifare_UltraLight_C;
                case 0x003B: return CardType.Desfire;
                case 0xFFA0: return CardType.Generic_unknown_14443_A_card;
                case 0xFFA1: return CardType.Kovio_RF_barcode;
                case 0xFFB0: return CardType.Generic_unknown_14443_B_card;
                case 0xFFB1: return CardType.ASK_CTS_256B;
                case 0xFFB2: return CardType.ASK_CTS_512B;
                case 0xFFB3: return CardType.Pre_standard_ST_MicroElectronics_SRI_4K;
                case 0xFFB4: return CardType.Pre_standard_ST_MicroElectronics_SRI_X512;
                case 0xFFB5: return CardType.Pre_standard_ST_MicroElectronics_SRI_512;
                case 0xFFB6: return CardType.Pre_standard_ST_MicroElectronics_SRT_512;
                case 0xFFB7: return CardType.Inside_Contactless_PICOTAG_PICOPASS;
                case 0xFFB8: return CardType.Generic_Atmel_AT88SC_AT88RF_card;
                case 0xFFC0: return CardType.Calypso_card_using_the_Innovatron_protocol;
                case 0xFFD0: return CardType.Generic_ISO_15693_from_unknown_manufacturer;
                case 0xFFD1: return CardType.Generic_ISO_15693_from_EMMarin_or_Legic;
                case 0xFFD2: return CardType.Generic_ISO_15693_from_ST_MicroElectronics_block_number_on_8_bits;
                case 0xFFD3: return CardType.Generic_ISO_15693_from_ST_MicroElectronics_block_number_on_16_bits;
                case 0xFFFF: return CardType.Virtual_card;
                default: return CardType.Unknown_Card_Type;
            }
        }
    }
}
