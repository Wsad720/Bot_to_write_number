using System;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
    [DllImport("user32.dll")]
    static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

    const uint KEYEVENTF_KEYUP = 0x0002;
    const byte VK_RETURN = 0x0D;

    static readonly byte[] NumberKeys = new byte[]
    {
        0x30, 0x31, 0x32, 0x33, 0x34,
        0x35, 0x36, 0x37, 0x38, 0x39
    };

    static void Main()
    {
        Console.Write("Pleas write start value: ");
        int current = int.Parse(Console.ReadLine()) + 1;

        int time = 500;
        Console.Write("Pleas write repeat time:");
        time = int.Parse(Console.ReadLine());

        Console.Write("Are you ready? (Y/N): ");
        string response = Console.ReadLine()?.Trim().ToUpper();

        if (response != "Y")
        {
            Console.WriteLine("bot start canceled.");
            return;
        }

        Console.WriteLine("Start in 5 secound...");
        Thread.Sleep(5000);

        while (true)
        {
            TypeNumber(current);
            PressKey(VK_RETURN);
            current++;
            Thread.Sleep(time); // 500ms
        }
    }

    static void TypeNumber(int number)
    {
        foreach (char digit in number.ToString())
        {
            byte key = NumberKeys[digit - '0'];
            PressKey(key);
        }
    }

    static void PressKey(byte key)
    {
        keybd_event(key, 0, 0, UIntPtr.Zero);                
        keybd_event(key, 0, KEYEVENTF_KEYUP, UIntPtr.Zero); 
        Thread.Sleep(50);
    }
}
