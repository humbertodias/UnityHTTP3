﻿using System.Net; 
using System.Net.Sockets; 
using System.Text; 
 
public class UDPListener 
{ 
    private const int listenPort = 11001; 
 
    private static void StartListener() 
    { 
        UdpClient listener = new UdpClient(listenPort); 
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort); 
 
        try 
        { 
            while (true) 
            { 
                Console.WriteLine("Waiting for broadcast"); 
                byte[] bytes = listener.Receive(ref groupEP); 
 
                Console.WriteLine($"Received broadcast from {groupEP} :"); 
                Console.WriteLine($" {Encoding.UTF8.GetString(bytes, 0, bytes.Length)}"); 
            } 
        } 
        catch (SocketException e) 
        { 
            Console.WriteLine(e); 
        } 
        finally 
        { 
            listener.Close(); 
        } 
    } 
 
    public static void Main() 
    { 
        StartListener(); 
    } 
}