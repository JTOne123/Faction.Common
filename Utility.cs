using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Faction.Common
{
    public class FactionSettings
    {
    public string POSTGRES_HOST;
    public string POSTGRES_DATABASE;
    public string POSTGRES_USERNAME;
    public string POSTGRES_PASSWORD;
    public string RABBIT_HOST;
    public string RABBIT_USERNAME;
    public string RABBIT_PASSWORD;
    public string SYSTEM_USERNAME;
    public string SYSTEM_PASSWORD;

    public bool IsPopulated() {
        if (String.IsNullOrEmpty(POSTGRES_HOST) || 
            String.IsNullOrEmpty(POSTGRES_DATABASE) || 
            String.IsNullOrEmpty(POSTGRES_USERNAME) || 
            String.IsNullOrEmpty(POSTGRES_PASSWORD) ||
            String.IsNullOrEmpty(RABBIT_HOST) ||
            String.IsNullOrEmpty(RABBIT_USERNAME) || 
            String.IsNullOrEmpty(RABBIT_PASSWORD) || 
            String.IsNullOrEmpty(SYSTEM_USERNAME) || 
            String.IsNullOrEmpty(SYSTEM_PASSWORD))
        {
            return false;
        }
        else {
        return true;
      }
    }
  }
  public static class Utility
  {
    public static FactionSettings GetConfiguration(string path = "/opt/faction/global/config.json")
    {
      FactionSettings factionSettings = new FactionSettings();
      factionSettings.POSTGRES_DATABASE = Environment.GetEnvironmentVariable("POSTGRES_DATABASE");
        factionSettings.POSTGRES_USERNAME = Environment.GetEnvironmentVariable("POSTGRES_USERNAME");
        factionSettings.POSTGRES_PASSWORD = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
        factionSettings.POSTGRES_HOST = Environment.GetEnvironmentVariable("POSTGRES_HOST");
        factionSettings.RABBIT_USERNAME = Environment.GetEnvironmentVariable("RABBIT_USERNAME");
        factionSettings.RABBIT_PASSWORD = Environment.GetEnvironmentVariable("RABBIT_PASSWORD");
        factionSettings.RABBIT_HOST = Environment.GetEnvironmentVariable("RABBIT_HOST");
        factionSettings.SYSTEM_USERNAME = Environment.GetEnvironmentVariable("SYSTEM_USERNAME");
        factionSettings.SYSTEM_PASSWORD = Environment.GetEnvironmentVariable("SYSTEM_PASSWORD");
        if (!factionSettings.IsPopulated())
        {
            try 
            {
                string contents = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<FactionSettings>(contents);
            }
            catch (Exception e)
            {
                Console.WriteLine($"FATAL ERROR: Could not build config. Error: {e.Message}");
            }
        }
      return factionSettings;
    }

    public static string GenerateSecureString(int length, string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-_")
    {
      using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
      {
        byte[] data = new byte[length];

        // If chars.Length isn't a power of 2 then there is a bias if we simply use the modulus operator. The first characters of chars will be more probable than the last ones.
        // buffer used if we encounter an unusable random byte. We will regenerate it in this buffer
        byte[] buffer = null;

        // Maximum random number that can be used without introducing a bias
        int maxRandom = byte.MaxValue - ((byte.MaxValue + 1) % chars.Length);

        crypto.GetBytes(data);

        char[] result = new char[length];

        for (int i = 0; i < length; i++)
        {
          byte value = data[i];

          while (value > maxRandom)
          {
            if (buffer == null)
            {
              buffer = new byte[1];
            }

            crypto.GetBytes(buffer);
            value = buffer[0];
          }

          result[i] = chars[value % chars.Length];
        }

        return new string(result);
      }
    }
  }
}