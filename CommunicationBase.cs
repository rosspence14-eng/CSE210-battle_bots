using System;

namespace battlebots
{
    /*--------------------------------------------
    PARAMETERS:
        PortName (string): The communication port identifier (e.g., COM1, /dev/ttyUSB0)
        BaudRate (int): The data transmission speed in bits per second
        _isConnected_RP (bool): Whether the communication channel is currently open
        _sendMessageCount_RP (int): Number of messages sent through this communicator
        _receiveMessageCount_RP (int): Number of messages received through this communicator
    FUNCTION:
        Abstract base class providing core communication functionality.
        Implements ICommunicator interface for sending and receiving messages.
        Provides common logging, connection management, and error handling
        that all communicators (motor controllers, sensors, etc.) can use.
        
        SendMessage(string): Transmits a message through the communication port
        ReceiveMessage(): Retrieves incoming messages from the communication port
        OpenConnection(): Opens the communication channel
        CloseConnection(): Closes the communication channel
    OUTPUTS:
        SendMessage(): void - no return value, prints to console
        ReceiveMessage(): string - returns the received message
        OpenConnection(): void - no return value
        CloseConnection(): void - no return value
    ---------------------------------------------*/
    public abstract class CommunicationBase : ICommunicator
    {
        // The name of the communication port (e.g., "COM3", "/dev/ttyUSB0")
        public string PortName { get; set; }

        // The baud rate for data transmission (bits per second)
        public int BaudRate { get; set; }

        // Whether the communication channel is currently open and connected
        private bool _isConnected_RP;

        // Count of messages successfully sent
        private int _sendMessageCount_RP;

        // Count of messages successfully received
        private int _receiveMessageCount_RP;

        /// <summary>
        /// Constructor to initialize the communication base with default values
        /// </summary>
        public CommunicationBase()
        {
            PortName = "COM1";
            BaudRate = 9600;
            _isConnected_RP = false;
            _sendMessageCount_RP = 0;
            _receiveMessageCount_RP = 0;

            Console.WriteLine("[COMM] Communication base initialized");
        }

        /// <summary>
        /// Constructor to initialize with specific port and baud rate
        /// </summary>
        /// <param name="portName_RP">Communication port identifier</param>
        /// <param name="baudRate_RP">Data transmission speed in bps</param>
        public CommunicationBase(string portName_RP, int baudRate_RP)
        {
            PortName = portName_RP;
            BaudRate = baudRate_RP;
            _isConnected_RP = false;
            _sendMessageCount_RP = 0;
            _receiveMessageCount_RP = 0;

            Console.WriteLine($"[COMM] Communication base initialized: Port={PortName}, BaudRate={BaudRate}");
        }

        /// <summary>
        /// Sends a message through the communication port
        /// Logs the message and increments send counter
        /// </summary>
        /// <param name="message_RP">The message string to send</param>
        public virtual void SendMessage(string message_RP)
        {
            // Check if connection is open
            if (!_isConnected_RP)
            {
                Console.WriteLine($"[COMM] WARNING: Cannot send message - connection not open! Port: {PortName}");
                Console.WriteLine("[COMM] Attempting to auto-connect...");
                OpenConnection();
            }

            // Validate message
            if (string.IsNullOrEmpty(message_RP))
            {
                Console.WriteLine("[COMM] ERROR: Cannot send empty or null message!");
                return;
            }

            // Simulate sending the message
            _sendMessageCount_RP++;
            Console.WriteLine($"[COMM] >>> SEND [{_sendMessageCount_RP}]: \"{message_RP}\"");
            Console.WriteLine($"[COMM]     Port: {PortName} | Baud Rate: {BaudRate} bps");

            // Simulate occasional transmission errors (10% chance)
            Random random_RP = new Random();
            if (random_RP.Next(1, 11) == 1) // 1 in 10 chance
            {
                Console.WriteLine("[COMM] ERROR: Transmission failed! Retrying...");
                System.Threading.Thread.Sleep(100); // Simulate retry delay
                Console.WriteLine($"[COMM] >>> RETRY SEND: \"{message_RP}\" - Success!");
            }
        }

        /// <summary>
        /// Receives a message from the communication port
        /// Simulates incoming data and increments receive counter
        /// </summary>
        /// <returns>The received message string, or empty string if none available</returns>
        public virtual string ReceiveMessage()
        {
            // Check if connection is open
            if (!_isConnected_RP)
            {
                Console.WriteLine($"[COMM] WARNING: Cannot receive message - connection not open! Port: {PortName}");
                return "";
            }

            // Simulate receiving a message (with some randomness)
            Random random_RP = new Random();
            bool hasMessage_RP = random_RP.Next(1, 4) == 1; // 33% chance of having a message

            if (hasMessage_RP)
            {
                _receiveMessageCount_RP++;
                string receivedMessage_RP = $"ACK_{_receiveMessageCount_RP}";
                Console.WriteLine($"[COMM] <<< RECEIVE [{_receiveMessageCount_RP}]: \"{receivedMessage_RP}\"");
                return receivedMessage_RP;
            }
            else
            {
                // No message available
                return "";
            }
        }

        /// <summary>
        /// Opens the communication channel for data transmission
        /// </summary>
        public virtual void OpenConnection()
        {
            if (_isConnected_RP)
            {
                Console.WriteLine($"[COMM] Connection already open on port: {PortName}");
                return;
            }

            Console.WriteLine($"[COMM] Opening connection to port: {PortName}");
            Console.WriteLine($"[COMM] Setting baud rate: {BaudRate} bps");

            // Simulate connection establishment
            System.Threading.Thread.Sleep(200);

            _isConnected_RP = true;
            Console.WriteLine($"[COMM] Connection ESTABLISHED successfully!");
            Console.WriteLine($"[COMM] Port: {PortName} | Baud Rate: {BaudRate} bps | Status: CONNECTED");
        }

        /// <summary>
        /// Closes the communication channel and releases resources
        /// </summary>
        public virtual void CloseConnection()
        {
            if (!_isConnected_RP)
            {
                Console.WriteLine($"[COMM] Connection already closed for port: {PortName}");
                return;
            }

            Console.WriteLine($"[COMM] Closing connection to port: {PortName}");

            // Simulate cleanup
            System.Threading.Thread.Sleep(100);

            _isConnected_RP = false;
            Console.WriteLine("[COMM] Connection CLOSED");
            Console.WriteLine($"[COMM] Statistics - Messages Sent: {_sendMessageCount_RP}, Received: {_receiveMessageCount_RP}");
        }

        /// <summary>
        /// Checks if the communication channel is currently connected
        /// </summary>
        /// <returns>True if connected, false otherwise</returns>
        public bool IsConnected()
        {
            return _isConnected_RP;
        }

        /// <summary>
        /// Gets the number of messages sent through this communicator
        /// </summary>
        /// <returns>Message send count</returns>
        public int GetSendMessageCount()
        {
            return _sendMessageCount_RP;
        }

        /// <summary>
        /// Gets the number of messages received through this communicator
        /// </summary>
        /// <returns>Message receive count</returns>
        public int GetReceiveMessageCount()
        {
            return _receiveMessageCount_RP;
        }

        /// <summary>
        /// Gets a detailed status report of the communication channel
        /// </summary>
        /// <returns>Status string with connection details</returns>
        public string GetStatus()
        {
            string status_RP = "[COMM] === Communication Status ===\n";
            status_RP += $"  Port Name: {PortName}\n";
            status_RP += $"  Baud Rate: {BaudRate} bps\n";
            status_RP += $"  Connected: {_isConnected_RP}\n";
            status_RP += $"  Messages Sent: {_sendMessageCount_RP}\n";
            status_RP += $"  Messages Received: {_receiveMessageCount_RP}\n";
            status_RP += "[COMM] ============================";

            return status_RP;
        }

        /// <summary>
        /// Prints the current communication status to console
        /// </summary>
        public void PrintStatus()
        {
            Console.WriteLine(GetStatus());
        }

        /// <summary>
        /// Tests the communication link by sending and receiving a test message
        /// </summary>
        /// <returns>True if test successful, false otherwise</returns>
        public bool TestConnection()
        {
            Console.WriteLine("[COMM] Starting communication link test...");

            // Ensure connection is open
            if (!_isConnected_RP)
            {
                OpenConnection();
            }

            // Send test message
            string testMessage_RP = "TEST_PING";
            SendMessage(testMessage_RP);

            // Wait and try to receive response
            System.Threading.Thread.Sleep(200);
            string response_RP = ReceiveMessage();

            if (!string.IsNullOrEmpty(response_RP))
            {
                Console.WriteLine("[COMM] Link test PASSED - communication working!");
                return true;
            }
            else
            {
                Console.WriteLine("[COMM] Link test WARNING - no response received (this is normal in simulation)");
                return false;
            }
        }
    }
}