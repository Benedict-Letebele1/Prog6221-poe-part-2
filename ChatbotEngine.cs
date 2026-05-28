using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberSecurityChatbot_POE
{
    // Delegate required for Part 2
    public delegate string BotResponseDelegate(string input);

    public class ChatbotEngine
    {
        private string userName;
        private string favouriteTopic = "";
        private string currentTopic = "";

        private readonly Random random = new Random();

        // Part 1 logic preserved
        private readonly Dictionary<string, string> generalResponses;
        private readonly Dictionary<string, string> phishingResponses;
        private readonly Dictionary<string, string> passwordResponses;
        private readonly Dictionary<string, string> safeBrowsingResponses;

        // Part 2 additions
        private readonly Dictionary<string, List<string>> randomTopicResponses;
        private readonly Dictionary<string, List<string>> sentimentResponses;

        public ChatbotEngine(string userName)
        {
            this.userName = string.IsNullOrWhiteSpace(userName) ? "Friend" : userName;

            generalResponses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "how are you", $"I'm just a program, {this.userName}, but I'm here and ready to help you stay safe online!" },
                { "what is your purpose", $"My purpose is to educate you about cybersecurity, {this.userName}. I can answer questions about phishing, password safety, and safe browsing." },
                { "what can i ask you about", "You can ask me about phishing emails, password safety, safe browsing habits, privacy, scams, and general cybersecurity awareness." },
                { "who created you", "I was created as part of a cybersecurity awareness project for South African citizens." },
                { "why is cybersecurity important", "Cybersecurity is important because it protects your personal information, finances, and identity from online threats like hackers, scammers, and malware." },
                { "hello", $"Hello {this.userName}! How can I assist you with cybersecurity today?" },
                { "help", "I'm here to answer your cybersecurity questions. You can ask me about phishing, passwords, safe browsing, scams, privacy, or 2FA." }
            };

            phishingResponses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "what is phishing", "Phishing is a cyber attack where scammers impersonate legitimate organisations through email, SMS, calls, or fake websites to trick people into revealing sensitive information." },
                { "how to spot phishing email", "Look for urgent language, generic greetings, suspicious sender addresses, spelling mistakes, and links that do not match the real website." },
                { "what to do if i clicked a phishing link", "Disconnect from the internet, run an antivirus scan, change any passwords you entered, enable two-factor authentication, and monitor your accounts." },
                { "examples of phishing", "Common examples include fake bank emails, delivery scams, fake lottery winnings, and fake tech support messages." },
                { "what is smishing", "Smishing is phishing through SMS messages. Scammers send texts with malicious links or requests for personal information." },
                { "what is vishing", "Vishing is voice phishing, where scammers call pretending to be from your bank, tech support, or government services." },
                { "how to report phishing", "You can report phishing to the company being impersonated or to the relevant cybercrime authorities." },
                { "what are phishing red flags", "Red flags include urgent threats, poor grammar, mismatched URLs, requests for personal details, and offers that sound too good to be true." }
            };

            passwordResponses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "how to create strong password", "Use uppercase letters, lowercase letters, numbers, and special characters. Aim for at least 12 characters and avoid obvious personal details." },
                { "what is two factor authentication", "Two-factor authentication adds a second security step, such as a code from your phone or authenticator app, after your password." },
                { "how often to change passwords", "Change passwords immediately if you suspect a breach. For sensitive accounts, update them regularly and use unique passwords." },
                { "what is password manager", "A password manager stores and generates strong, unique passwords so you do not have to remember every password yourself." },
                { "should i reuse passwords", "No. Reusing passwords is dangerous because one hacked account can lead to many compromised accounts." },
                { "how to remember strong passwords", "Use a password manager or create a long passphrase that is memorable but hard to guess." },
                { "what is multi factor authentication", "Multi-factor authentication uses more than one proof of identity, such as something you know, something you have, or something you are." },
                { "common password mistakes", "Common mistakes include using password123, birthdays, names, qwerty, sticky notes, and sharing passwords with others." }
            };

            safeBrowsingResponses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "how to identify safe websites", "Look for HTTPS, check the domain spelling, avoid suspicious pop-ups, and make sure the website looks professional and trustworthy." },
                { "what is https", "HTTPS encrypts data between your browser and the website, making it harder for attackers to intercept your information." },
                { "how to avoid fake websites", "Type URLs manually, use bookmarks for important sites, double-check domains, and avoid suspicious ads or unrealistic offers." },
                { "what are cookies safe", "Cookies are usually small data files, but tracking cookies can follow your activity. Clear cookies regularly and block third-party cookies where possible." },
                { "how to browse safely on public wifi", "Avoid logging into sensitive accounts on public Wi-Fi. Use a VPN, turn off file sharing, and forget the network after use." },
                { "what is incognito mode", "Incognito mode hides local browsing history, but it does not make you anonymous to websites, your ISP, or network administrators." },
                { "how to check if link is safe", "Hover over the link, inspect the URL, use a link scanner, and avoid clicking if the link looks strange." },
                { "what is browser security", "Browser security includes updates, pop-up blocking, ad-blockers, anti-tracking tools, and avoiding suspicious downloads." }
            };

            // ================= PART 2: RANDOM RESPONSES =================
            randomTopicResponses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                {
                    "password",
                    new List<string>
                    {
                        passwordResponses["how to create strong password"],
                        passwordResponses["what is password manager"],
                        passwordResponses["should i reuse passwords"],
                        "Use different passwords for different accounts. One password everywhere is asking for trouble."
                    }
                },
                {
                    "phishing",
                    new List<string>
                    {
                        phishingResponses["what is phishing"],
                        phishingResponses["how to spot phishing email"],
                        phishingResponses["what are phishing red flags"],
                        "Never trust urgent messages blindly. Scammers love panic because panic makes people careless."
                    }
                },
                {
                    "scam",
                    new List<string>
                    {
                 
                        "Scams often pressure you to act quickly. Slow down, verify, and never share sensitive details blindly.",
                        "If someone asks for money, OTPs, passwords, or banking details unexpectedly, treat it as suspicious.",
                        "Too-good-to-be-true offers are usually bait. Verify before clicking or paying."
                    }
                },
                {
                    "privacy",
                    new List<string>
                    {
                        "Privacy means controlling what information you share online and who can see it.",
                        "Review privacy settings on social media accounts regularly.",
                        "Avoid sharing your address, ID number, school, workplace routine, or live location online."
                    }
                },
                {
                    "safe browsing",
                    new List<string>
                    {
                        safeBrowsingResponses["how to identify safe websites"],
                        safeBrowsingResponses["how to avoid fake websites"],
                        safeBrowsingResponses["how to browse safely on public wifi"]
                    }
                },
                {
                    "2fa",
                    new List<string>
                    {
                        passwordResponses["what is two factor authentication"],
                        "2FA protects your account even if your password gets stolen.",
                        "Use an authenticator app where possible because it is safer than SMS."
                    }
                }
            };

            // ================= PART 2: SENTIMENT DETECTION =================
            sentimentResponses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                {
                    "worried",
                    new List<string>
                    {
                        "It is understandable to feel worried. Cybersecurity can feel overwhelming, but you can reduce risk one step at a time.",
                        "Do not panic. Start with strong passwords, 2FA, and avoiding suspicious links."
                    }
                },
                {
                    "frustrated",
                    new List<string>
                    {
                        "I get it. Cybersecurity can feel annoying, but protecting your accounts is worth the effort.",
                        "That frustration makes sense. Let us simplify things and focus on one practical safety step."
                    }
                },
                {
                    "curious",
                    new List<string>
                    {
                        "Curiosity is good. Learning how cyber threats work makes you harder to trick.",
                        "Good mindset. Ask about phishing, passwords, scams, privacy, safe browsing, or 2FA."
                    }
                }
            };
        }

        public string GetResponse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "Please type something so I can help you.";
            }

            input = input.ToLower().Trim();

            // Delegate used for sentiment detection
            BotResponseDelegate sentimentChecker = DetectSentiment;
            string sentimentResponse = sentimentChecker(input);

            string memoryResponse = DetectMemory(input);
            if (!string.IsNullOrWhiteSpace(memoryResponse))
            {
                return memoryResponse;
            }

            string exactPartOneResponse = DetectPartOneResponse(input);
            string topicResponse = DetectTopic(input);

            if (!string.IsNullOrWhiteSpace(sentimentResponse) &&
                !string.IsNullOrWhiteSpace(topicResponse))
            {
                return sentimentResponse + Environment.NewLine + Environment.NewLine + topicResponse;
            }

            if (!string.IsNullOrWhiteSpace(exactPartOneResponse))
            {
                return exactPartOneResponse;
            }

            if (!string.IsNullOrWhiteSpace(sentimentResponse))
            {
                return sentimentResponse;
            }

            if (!string.IsNullOrWhiteSpace(topicResponse))
            {
                return topicResponse;
            }

            if (input.Contains("tell me more") ||
                input.Contains("another tip") ||
                input.Contains("explain more"))
            {
                return GiveFollowUpResponse();
            }

            if (!string.IsNullOrWhiteSpace(favouriteTopic))
            {
                return "I'm not sure I understand. Since you are interested in " +
                       favouriteTopic + ", you can ask me for more tips about that topic.";
            }

            return "I didn’t quite understand that. Could you rephrase?";
        }

        private string DetectPartOneResponse(string input)
        {
            string response;

            response = GetDictionaryResponse(input, generalResponses);
            if (response != null) return response;

            response = GetDictionaryResponse(input, phishingResponses);
            if (response != null)
            {
                currentTopic = "phishing";
                return response;
            }

            response = GetDictionaryResponse(input, passwordResponses);
            if (response != null)
            {
                currentTopic = "password";
                return response;
            }

            response = GetDictionaryResponse(input, safeBrowsingResponses);
            if (response != null)
            {
                currentTopic = "safe browsing";
                return response;
            }

            return "";
        }

        private string DetectTopic(string input)
        {
            foreach (string topic in randomTopicResponses.Keys)
            {
                if (input.Contains(topic))
                {
                    currentTopic = topic;
                    return GetRandomResponse(randomTopicResponses[topic]);
                }
            }

            return "";
        }

        private string DetectSentiment(string input)
        {
            foreach (string sentiment in sentimentResponses.Keys)
            {
                if (input.Contains(sentiment))
                {
                    return GetRandomResponse(sentimentResponses[sentiment]);
                }
            }

            return "";
        }

        private string DetectMemory(string input)
        {
            if (input.Contains("my name is"))
            {
                userName = input.Replace("my name is", "").Trim();

                if (string.IsNullOrWhiteSpace(userName))
                {
                    userName = "Friend";
                }

                return "Got it. I’ll call you " + userName + ".";
            }

            if (input.Contains("interested in"))
            {
                foreach (string topic in randomTopicResponses.Keys)
                {
                    if (input.Contains(topic))
                    {
                        favouriteTopic = topic;
                        currentTopic = topic;

                        return "Great, " + userName + ". I’ll remember that you are interested in " +
                               favouriteTopic + ". It is an important part of staying safe online.";
                    }
                }
            }

            if (input.Contains("remember") ||
                input.Contains("what do you know about me"))
            {
                if (!string.IsNullOrWhiteSpace(favouriteTopic))
                {
                    return "I remember that your name is " + userName +
                           " and you are interested in " + favouriteTopic + ".";
                }

                return "I remember that your name is " + userName + ".";
            }

            return "";
        }

        private string GiveFollowUpResponse()
        {
            if (string.IsNullOrWhiteSpace(currentTopic))
            {
                return "Tell me which topic you want more information about: phishing, password, privacy, scam, safe browsing, or 2FA.";
            }

            if (randomTopicResponses.ContainsKey(currentTopic))
            {
                return GetRandomResponse(randomTopicResponses[currentTopic]);
            }

            return "Ask me about phishing, password safety, privacy, scams, safe browsing, or 2FA.";
        }

        private string GetDictionaryResponse(string userInput, Dictionary<string, string> responses)
        {
            string normalized = userInput.ToLower().Trim();

            if (responses.ContainsKey(normalized))
            {
                return responses[normalized];
            }

            foreach (string key in responses.Keys)
            {
                if (normalized.Contains(key))
                {
                    return responses[key];
                }
            }

            return null;
        }

        private string GetRandomResponse(List<string> responses)
        {
            int index = random.Next(responses.Count);
            return responses[index];
        }

        public string GetAsciiArt()
        {
            return @"
╔══════════════════════════════════════╗
║        🔒 CYBER GUARDIAN 🔒          ║
║  Cybersecurity Awareness Assistant   ║
╚══════════════════════════════════════╝";
        }
    }
}