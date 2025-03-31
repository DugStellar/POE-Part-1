using System;
using System.IO;
using NAudio.Wave;

public class InfoGuard
{
    public string UserName { get; set; }

    public void Run()
    {
        PlayVoiceGreeting();
        DisplayAsciiArt();
        GreetUser();
        HandleUserInteraction();
    }

    private void PlayVoiceGreeting()
    {
        try
        {
            string soundFilePath = "greeting.wav";

            if (File.Exists(soundFilePath))
            {
                try
                {
                    using (var audioFile = new AudioFileReader(soundFilePath))
                    using (var outputDevice = new WaveOutEvent())
                    {
                        outputDevice.Init(audioFile);
                        outputDevice.Play();
                        while (outputDevice.PlaybackState == PlaybackState.Playing)
                        {
                            System.Threading.Thread.Sleep(100);
                        }
                    }
                }
                catch (Exception audioEx)
                {
                    Console.WriteLine($"Error during audio playback: {audioEx.Message}");
                }

            }
            else
            {
                Console.WriteLine("Warning: Voice greeting file not found. Place 'greeting.wav' in the application's directory.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error accessing audio file: {ex.Message}");
        }
    }

    private void DisplayAsciiArt()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(@"
    /\_/\  
  ( o.o )  
    > ^ <  InfoGuard
  /  _  \  
 | (_) |  Meow! Let's stay safe online!
 \_____/  
");
        Console.ResetColor();
    }

    private void GreetUser()
    {
        Console.Write("Please enter your name: ");
        UserName = Console.ReadLine();

        if (string.IsNullOrEmpty(UserName))
        {
            UserName = "User";
        }

        Console.WriteLine($"\nHello, {UserName}! Welcome to InfoGuard.");
    }

    private void HandleUserInteraction()
    {
        while (true)
        {
            Console.Write("\nAsk me a question (or type 'exit' to quit): ");
            string userInput = Console.ReadLine();

            if (userInput.ToLower() == "exit")
            {
                Console.WriteLine("Goodbye! Stay safe online.");
                break;
            }

            string response = GetResponse(userInput);
            Console.WriteLine(response);
        }
    }

    private string GetResponse(string userInput)
    {
        userInput = userInput.ToLower();

        if (userInput.Contains("how are you"))
        {
            return "I'm doing well, thank you! Ready to help you with cybersecurity.";
        }
        else if (userInput.Contains("what's your purpose"))
        {
            return "My purpose is to educate you about cybersecurity best practices.";
        }
        else if (userInput.Contains("what can i ask you about"))
        {
            return "You can ask me about:\n" +
                   "- Password safety\n" +
                   "- Phishing\n" +
                   "- Safe browsing\n" +
                   "- And other cybersecurity topics!";
        }
        else if (userInput.Contains("password safety"))
        {
            return "For strong passwords, use a combination of uppercase and lowercase letters, numbers, and symbols. Avoid personal information and use a password manager.";
        }
        else if (userInput.Contains("phishing"))
        {
            return "Phishing involves deceptive emails or messages that try to trick you into revealing sensitive information. Be cautious of suspicious links and emails.";
        }
        else if (userInput.Contains("safe browsing"))
        {
            return "Always use secure websites (HTTPS), avoid clicking on suspicious links, and keep your browser updated.";
        }
        else if (string.IsNullOrWhiteSpace(userInput))
        {
            return "Please enter a question.";
        }
        else
        {
            return "I didn't quite understand that. Could you rephrase?";
        }
    }

    public static void Main(string[] args)
    {
        InfoGuard infoGuard = new InfoGuard();
        infoGuard.Run();
    }
}