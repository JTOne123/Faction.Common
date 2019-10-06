using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Faction.Common.Utilities
{
  public class FactionSettings
  {
    public static string POSTGRES_HOST;
    public static string POSTGRES_DATABASE;
    public static string POSTGRES_USERNAME;
    public static string POSTGRES_PASSWORD;
    public static string RABBIT_HOST;
    public static string RABBIT_USERNAME;
    public static string RABBIT_PASSWORD;
    public static string SYSTEM_USERNAME;
    public static string SYSTEM_PASSWORD;
    public static string MINIO_ACCESSKEY;
    public static string MINIO_SECRETKEY;

    public static bool IsPopulated() {
      if (String.IsNullOrEmpty(POSTGRES_HOST) || 
          String.IsNullOrEmpty(POSTGRES_DATABASE) || 
          String.IsNullOrEmpty(POSTGRES_USERNAME) || 
          String.IsNullOrEmpty(POSTGRES_PASSWORD) ||
          String.IsNullOrEmpty(RABBIT_HOST) ||
          String.IsNullOrEmpty(RABBIT_USERNAME) || 
          String.IsNullOrEmpty(RABBIT_PASSWORD) || 
          String.IsNullOrEmpty(SYSTEM_USERNAME) || 
          String.IsNullOrEmpty(SYSTEM_PASSWORD) || 
          String.IsNullOrEmpty(MINIO_ACCESSKEY) || 
          String.IsNullOrEmpty(MINIO_SECRETKEY))
      {
        return false;
      }
      else {
        return true;
      }
    }

    public static void PopulateConfiguration(string path = "/opt/faction/global/config.json")
    {

      POSTGRES_DATABASE = Environment.GetEnvironmentVariable("POSTGRES_DATABASE");
      POSTGRES_USERNAME = Environment.GetEnvironmentVariable("POSTGRES_USERNAME");
      POSTGRES_PASSWORD = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
      POSTGRES_HOST = Environment.GetEnvironmentVariable("POSTGRES_HOST");
      RABBIT_USERNAME = Environment.GetEnvironmentVariable("RABBIT_USERNAME");
      RABBIT_PASSWORD = Environment.GetEnvironmentVariable("RABBIT_PASSWORD");
      RABBIT_HOST = Environment.GetEnvironmentVariable("RABBIT_HOST");
      SYSTEM_USERNAME = Environment.GetEnvironmentVariable("SYSTEM_USERNAME");
      SYSTEM_PASSWORD = Environment.GetEnvironmentVariable("SYSTEM_PASSWORD");
      MINIO_ACCESSKEY = Environment.GetEnvironmentVariable("MINIO_ACCESSKEY");
      MINIO_SECRETKEY = Environment.GetEnvironmentVariable("MINIO_SECRETKEY");

      if (IsPopulated()) return;

      try
      {
        string contents = File.ReadAllText(path);
        Dictionary<string, string> jsonSetting = JsonConvert.DeserializeObject<Dictionary<string, string>>(contents);

        POSTGRES_DATABASE = jsonSetting["POSTGRES_DATABASE"];
        POSTGRES_USERNAME = jsonSetting["POSTGRES_USERNAME"];
        POSTGRES_PASSWORD = jsonSetting["POSTGRES_PASSWORD"];
        POSTGRES_HOST = jsonSetting["POSTGRES_HOST"];
        RABBIT_USERNAME = jsonSetting["RABBIT_USERNAME"];
        RABBIT_PASSWORD = jsonSetting["RABBIT_PASSWORD"];
        RABBIT_HOST = jsonSetting["RABBIT_HOST"];
        SYSTEM_USERNAME = jsonSetting["SYSTEM_USERNAME"];
        SYSTEM_PASSWORD = jsonSetting["SYSTEM_PASSWORD"];
        MINIO_ACCESSKEY = jsonSetting["MINIO_ACCESSKEY"];
        MINIO_SECRETKEY = jsonSetting["MINIO_SECRETKEY"];
      }
      catch (Exception e)
      {
        Console.WriteLine($"FATAL ERROR: Could not build config. Error: {e.Message}");
      }
    }
    public FactionSettings()
    {
      PopulateConfiguration();
    }
  }
}