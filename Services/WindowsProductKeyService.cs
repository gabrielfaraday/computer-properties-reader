using System;
using System.Collections;
using computer_check.Models;
using Microsoft.Win32;

namespace computer_check.Services
{
    public class WindowsProductKeyService
    {
        public static void ReadKey(Computer computer)
        {
            try
            {
                Console.WriteLine("Starting reading Windows Product Key...");

                var key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);
                const string keyPath = @"Software\Microsoft\Windows NT\CurrentVersion";
                var digitalProductId = (byte[])key.OpenSubKey(keyPath).GetValue("DigitalProductId");

                var isWin8OrUp = (Environment.OSVersion.Version.Major == 6 &&
                                  System.Environment.OSVersion.Version.Minor >= 2) ||
                                 (Environment.OSVersion.Version.Major > 6);

                if (isWin8OrUp)
                    DecodeProductKeyWin8AndUp(computer, digitalProductId);
                else
                    DecodeProductKeyOldWindows(computer, digitalProductId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while reading Windowns Product Key properties: {ex.ToString()}");
            }

        }

        private static void DecodeProductKeyWin8AndUp(Computer computer, byte[] digitalProductId)
        {
            var key = String.Empty;
            const int keyOffset = 52;
            var isWin8 = (byte)((digitalProductId[66] / 6) & 1);
            digitalProductId[66] = (byte)((digitalProductId[66] & 0xf7) | (isWin8 & 2) * 4);

            const string digits = "BCDFGHJKMPQRTVWXY2346789";
            int last = 0;
            for (var i = 24; i >= 0; i--)
            {
                var current = 0;
                for (var j = 14; j >= 0; j--)
                {
                    current = current * 256;
                    current = digitalProductId[j + keyOffset] + current;
                    digitalProductId[j + keyOffset] = (byte)(current / 24);
                    current = current % 24;
                    last = current;
                }

                key = digits[current] + key;
            }

            computer.UndecodedWindowsProductKey = key;

            var keypart1 = key.Substring(1, last);
            const string insert = "N";
            key = key.Substring(1).Replace(keypart1, keypart1 + insert);

            if (last == 0)
                key = insert + key;

            for (var i = 5; i < key.Length; i += 6)
            {
                key = key.Insert(i, "-");
            }

            computer.DecodedWindowsProductKey = key;
        }

        private static void DecodeProductKeyOldWindows(Computer computer, byte[] digitalProductId)
        {
            const int keyStartIndex = 52;
            const int keyEndIndex = keyStartIndex + 15;
            var digits = new[]
            {
                'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'M', 'P', 'Q', 'R',
                'T', 'V', 'W', 'X', 'Y', '2', '3', '4', '6', '7', '8', '9',
            };
            const int decodeLength = 29;
            const int decodeStringLength = 15;
            var decodedChars = new char[decodeLength];
            var hexPid = new ArrayList();

            for (var i = keyStartIndex; i <= keyEndIndex; i++)
            {
                hexPid.Add(digitalProductId[i]);
            }

            computer.UndecodedWindowsProductKey = hexPid.ToString();

            for (var i = decodeLength - 1; i >= 0; i--)
            {
                if ((i + 1) % 6 == 0)
                {
                    decodedChars[i] = '-';
                }
                else
                {
                    var digitMapIndex = 0;
                    for (var j = decodeStringLength - 1; j >= 0; j--)
                    {
                        var byteValue = (digitMapIndex << 8) | (byte)hexPid[j];
                        hexPid[j] = (byte)(byteValue / 24);
                        digitMapIndex = byteValue % 24;
                        decodedChars[i] = digits[digitMapIndex];
                    }
                }
            }

            computer.DecodedWindowsProductKey = decodedChars.ToString();
        }
    }
}