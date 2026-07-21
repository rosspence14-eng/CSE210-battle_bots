using System;

namespace battlebots
{
    /*--------------------------------------------
    PARAMETERS:
        None - interface definition only
    FUNCTION:
        Defines the contract for communication functionality.
        Any class that needs to send and receive data must implement
        this interface to ensure consistent communication behavior
        across different hardware components (motors, sensors, remotes).
        
        SendMessage(string): Declares method to send messages through
                           a communication channel (serial, bluetooth, wifi, etc.)
        ReceiveMessage(): Declares method to receive messages from a
                        communication channel
    OUTPUTS:
        SendMessage(): void (to be implemented by derived classes)
        ReceiveMessage(): string (to be implemented by derived classes)
    ---------------------------------------------*/
    public interface ICommunicator
    {
        /// <summary>
        /// The name or identifier of the communication port
        /// (e.g., "COM3", "/dev/ttyUSB0", "Bluetooth-HC-05")
        /// </summary>
        string PortName { get; set; }

        /// <summary>
        /// The data transmission speed in bits per second (bps)
        /// Common values: 9600, 19200, 38400, 57600, 115200
        /// </summary>
        int BaudRate { get; set; }

        /// <summary>
        /// Sends a message through the communication channel
        /// Implementation should handle serial port, network socket, or other transport
        /// </summary>
        /// <param name="message_RP">The string message to transmit</param>
        void SendMessage(string message_RP);

        /// <summary>
        /// Receives a message from the communication channel
        /// Implementation should read from serial port, network socket, or other transport
        /// </summary>
        /// <returns>The received message as a string, or empty string if no message available</returns>
        string ReceiveMessage();

        /// <summary>
        /// Checks if the communication channel is currently connected and active
        /// </summary>
        /// <returns>True if connected, false otherwise</returns>
        bool IsConnected();

        /// <summary>
        /// Opens the communication channel for data transmission
        /// </summary>
        void OpenConnection();

        /// <summary>
        /// Closes the communication channel and releases resources
        /// </summary>
        void CloseConnection();
    }
}