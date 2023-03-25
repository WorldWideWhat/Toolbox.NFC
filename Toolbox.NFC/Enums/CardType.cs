﻿namespace Toolbox.NFC.Enums
{
    /// <summary>
    /// Available card types
    /// </summary>
    public enum CardType
    {
        Mifare_Standard_1k,
        Mifare_Standard_4k,
        Mifare_UltraLight,
        ST_MicroElectronics_SR176,
        ST_MicroElectronics_SRI4K_SRIX4K_SRIX512_SRI512_SRT512,
        Atmel_AT88SC0808CRF,
        Atmel_AT88SC1616CRF,
        Atmel_AT88SC3216CRF,
        Atmel_AT88SC6416CRF,
        Texas_Intruments_TAG_IT,
        ST_MicroElectronics_LRI512,
        ICODE_SLI,
        ICODE1,
        ST_MicroElectronics_LRI64,
        ST_MicroElectronics_LR12,
        ST_MicroElectronics_LRI128,
        Mifare_Mini,
        Innovision_Jewel,
        Innovision_Topaz,
        Atmel_AT88RF04C,
        ICODE_SL2,
        Mifare_UltraLight_C,
        Generic_unknown_14443_A_card,
        Kovio_RF_barcode,
        Generic_unknown_14443_B_card,
        ASK_CTS_256B,
        ASK_CTS_512B,
        Pre_standard_ST_MicroElectronics_SRI_4K,
        Pre_standard_ST_MicroElectronics_SRI_X512,
        Pre_standard_ST_MicroElectronics_SRI_512,
        Pre_standard_ST_MicroElectronics_SRT_512,
        Inside_Contactless_PICOTAG_PICOPASS,
        Generic_Atmel_AT88SC_AT88RF_card,
        Calypso_card_using_the_Innovatron_protocol,
        Generic_ISO_15693_from_unknown_manufacturer,
        Generic_ISO_15693_from_EMMarin_or_Legic,
        Generic_ISO_15693_from_ST_MicroElectronics_block_number_on_8_bits,
        Generic_ISO_15693_from_ST_MicroElectronics_block_number_on_16_bits,
        Virtual_card,
        Desfire,
        Mifare_Ultralight_family,
        Unknown_Card_Type,
        No_Card_Attached,
    }
}
