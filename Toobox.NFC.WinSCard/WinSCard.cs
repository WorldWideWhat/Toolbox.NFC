using System.Runtime.InteropServices;

namespace Toobox.NFC.WinSCard
{
    public sealed class WinSCardInterop
    {
        /*
         * Create Interops for the WinSCard.dll file
         */
        #region WinScard.dll
        [DllImport("WinScard.dll")]
        public static extern ErrorCode SCardEstablishContext(uint dwScope,
        IntPtr notUsed1,
        IntPtr notUsed2,
        out IntPtr phContext);


        // *********************************************************************************************************
        // Function Name: SCardReleaseContext
        // In Parameter : phContext - A handle to the established resource manager context              
        // Out Parameter: -------
        // Description  :Releases context from the reader
        //*********************************************************************************************************
        [DllImport("WinScard.dll")]
        public static extern ErrorCode SCardReleaseContext(IntPtr phContext);


        // *********************************************************************************************************
        // Function Name: SCardConnect
        // In Parameter : hContext - A handle that identifies the resource manager context.
        //                cReaderName  - The name of the reader that contains the target card.
        //                dwShareMode - A flag that indicates whether other applications may form connections to the card.
        //                dwPrefProtocol - A bitmask of acceptable protocols for the connection.  
        // Out Parameter: ActiveProtocol - A flag that indicates the established active protocol.
        //                hCard - A handle that identifies the connection to the smart card in the designated reader. 
        // Description  : Connect to card on reader
        //*********************************************************************************************************
        [DllImport("WinScard.dll")]
        public static extern ErrorCode SCardConnect(
            IntPtr hContext,
            string cReaderName,
            uint dwShareMode,
            uint dwPrefProtocol,
            ref IntPtr hCard,
            ref IntPtr ActiveProtocol);


        // *********************************************************************************************************
        // Function Name: SCardDisconnect
        // In Parameter : hCard - Reference value obtained from a previous call to SCardConnect.
        //                Disposition - Action to take on the card in the connected reader on close.  
        // Out(Parameter)
        // Description  : Disconnect card from reader
        //*********************************************************************************************************
        [DllImport("WinScard.dll")]
        public static extern ErrorCode SCardDisconnect(IntPtr hCard, int Disposition);


        //    *********************************************************************************************************
        // Function Name: SCardListReaders
        // In Parameter : hContext - A handle to the established resource manager context
        //                mszReaders - Multi-string that lists the card readers with in the supplied readers groups
        //                pcchReaders - length of the readerlist buffer in characters
        // Out Parameter: mzGroup - Names of the Reader groups defined to the System
        //                pcchReaders - length of the readerlist buffer in characters
        // Description  : List of all readers connected to system 
        //*********************************************************************************************************
        [DllImport("WinScard.dll", EntryPoint = "SCardListReadersA", CharSet = CharSet.Ansi)]
        public static extern ErrorCode SCardListReaders(
          IntPtr hContext,
          byte[] mszGroups,
          byte[] mszReaders,
          ref UInt32 pcchReaders
          );


        // *********************************************************************************************************
        // Function Name: SCardState
        // In Parameter : hCard - Reference value obtained from a previous call to SCardConnect.
        // Out Parameter: state - Current state of smart card in  the reader
        //                protocol - Current Protocol
        //                ATR - 32 bytes buffer that receives the ATR string
        //                ATRLen - Supplies the length of ATR buffer
        // Description  : Current state of the smart card in the reader
        //*********************************************************************************************************
        [DllImport("WinScard.dll")]
        public static extern ErrorCode SCardState(IntPtr hCard, ref IntPtr state, ref IntPtr protocol, ref Byte[] ATR, ref int ATRLen);


        // *********************************************************************************************************
        // Function Name: SCardTransmit
        // In Parameter : hCard - A reference value returned from the SCardConnect function.
        //                pioSendRequest - A pointer to the protocol header structure for the instruction.
        //                SendBuff- A pointer to the actual data to be written to the card.
        //                SendBuffLen - The length, in bytes, of the pbSendBuffer parameter. 
        //                pioRecvRequest - Pointer to the protocol header structure for the instruction ,Pointer to the protocol header structure for the instruction, 
        //                followed by a buffer in which to receive any returned protocol control information (PCI) specific to the protocol in use.
        //                RecvBuffLen - Supplies the length, in bytes, of the pbRecvBuffer parameter and receives the actual number of bytes received from the smart card.
        // Out Parameter: pioRecvRequest - Pointer to the protocol header structure for the instruction ,Pointer to the protocol header structure for the instruction, 
        //                followed by a buffer in which to receive any returned protocol control information (PCI) specific to the protocol in use.
        //                RecvBuff - Pointer to any data returned from the card.
        //                RecvBuffLen - Supplies the length, in bytes, of the pbRecvBuffer parameter and receives the actual number of bytes received from the smart card.
        // Description  : Transmit APDU to card 
        //*********************************************************************************************************

        [DllImport("WinScard.dll")]
        public static extern ErrorCode SCardTransmit(IntPtr hCard, ref SCard_IO_Request pioSendRequest,
                                                           ref Byte SendBuff,
                                                           int SendBuffLen,
                                                           ref SCard_IO_Request pioRecvRequest,
                                                           ref Byte RecvBuff, ref int RecvBuffLen);

        /*
        [DllImport("WinScard.dll")]
        private static extern int SCardTransmit(IntPtr hCard, ref HiDWinscard.SCARD_IO_REQUEST pioSendPci, byte[] pbSendBuffer,
                                               int cbSendLength, IntPtr pioRecvPci, byte[] pbRecvBuffer, ref int pcbRecvLength);
        */

        // *********************************************************************************************************
        // Function Name: SCardGetStatusChange
        // In Parameter : hContext - A handle that identifies the resource manager context.
        //                value_TimeOut - The maximum amount of time, in milliseconds, to wait for an action.
        //                ReaderState -  An array of SCARD_READERSTATE structures that specify the readers to watch, and that receives the result.
        //                ReaderCount -  The number of elements in the rgReaderStates array.
        // Out Parameter: ReaderState - An array of SCARD_READERSTATE structures that specify the readers to watch, and that receives the result.
        // Description  : The current availability of the cards in a specific set of readers changes.
        //*********************************************************************************************************
        [DllImport("winscard.dll", CharSet = CharSet.Unicode)]
        public static extern ErrorCode SCardGetStatusChange(IntPtr hContext,
        int value_Timeout,
        ref SCard_ReaderState ReaderState,
        uint ReaderCount);
        /*
        [DllImport("winscard.dll")]
        public static extern int SCardGetAttrib(IntPtr hCard, int dwAttrId, out IntPtr pbAttr, ref int pcbAttrLen);

        [DllImport("winscard.dll")]
        public static extern int SCardGetAttrib(IntPtr hCard, int dwAttrId, out uint pbAttr, ref int pcbAttrLen);
        */
        [DllImport("winscard.dll", SetLastError = true)]
        public static extern ErrorCode SCardGetAttrib(
           IntPtr hCard,            // Reference value returned from SCardConnect
           UInt32 dwAttrId,         // Identifier for the attribute to get
           byte[] pbAttr,           // Pointer to a buffer that receives the attribute
           ref IntPtr pcbAttrLen    // Length of pbAttr in bytes
        );

        #endregion

        [DllImport("scardsyn.dll")]
        public static extern int SCardCLGetUID(IntPtr ulHandleCard, Byte[] pucUID, ref UInt32 ulUIDBufLen, ref UInt32 pulnByteUID);

        // Context Scope

        public const int SCARD_STATE_UNAWARE = 0x0;

        //The application is unaware about the curent state, This value results in an immediate return
        //from state transition monitoring services. This is represented by all bits set to zero

        public const int SCARD_SHARE_SHARED = 2;

        // Application will share this card with other 
        // applications.

        //   Disposition

        public const int SCARD_UNPOWER_CARD = 2; // Power down the card on close

        //   PROTOCOL

        public const uint SCARD_PROTOCOL_T0 = 0x1;                  // T=0 is the active protocol.
        public const uint SCARD_PROTOCOL_T1 = 0x2;                  // T=1 is the active protocol.
        public const uint SCARD_PROTOCOL_UNDEFINED = 0x0;
        public const uint SCARD_PROTOCOL_RAW = 0x10000;
        public const uint SCARD_PROTOCOL_Tx = 0x3;
        public const uint SCARD_PROTOCOL_DEFAULT = 0x80000000;
        public const uint SCARD_PROTOCOL_OPTIMAL = 0;


        public const int SCARD_ATTR_VENDOR_NAME = 0x100;
        public const int SCARD_ATTR_VENDOR_IFD_SERIAL_NO = 65795;
        //IO Request Control

        //Card Type
        public const int card_Type_Mifare_1K = 1;
        public const int card_Type_Mifare_4K = 2;

        public static byte[] ULCauthKeyDefalt = new byte[] { 0x49, 0x45, 0x4D, 0x4B, 0x41, 0x45, 0x52, 0x42, 0x21, 0x4E, 0x41, 0x43, 0x55, 0x4F, 0X59, 0x46 };


    }
}
