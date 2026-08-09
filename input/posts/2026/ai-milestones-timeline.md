Title: "A Timeline of AI Milestones"
Published: 07/12/2026
Lead: "A clean, visual journey through the history and milestones of artificial intelligence, featuring deep learning breakthroughs and agentic tools."
Tags:
- AI
- deep learning
- history
- tech industry
- timeline
- openai
- google
- anthropic
- xai
- ai-generated
---

# A Timeline of AI Milestones

> ⚠️ **Work in Progress:** Most of the content and analysis on this page was AI-generated with custom developer refinements. I will return to edit, verify, and further refine this timeline in the future.

Below is an interactive, theme-adaptive timeline detailing the acceleration of modern artificial intelligence milestones, from the coining of the term in the 1950s to the reasoning models and agentic workflows of today.

<div id="timeline-embed" style="width: 100%; height: 650px; margin: 2rem 0; border-radius: 12px; overflow: hidden; border: 1px solid rgba(255, 255, 255, 0.1);"></div>

<link rel="stylesheet" href="https://cdn.knightlab.com/libs/timeline3/latest/css/timeline.css">
<script src="https://cdn.knightlab.com/libs/timeline3/latest/js/timeline.js"></script>

<style>
  /* Base Overrides for TimelineJS in Dark Theme */
  .tl-timeline {
    background-color: #0f172a !important;
    color: #e2e8f0 !important;
  }
  .tl-slide {
    background-color: #0f172a !important;
  }
  .tl-navigation {
    background-color: #0b0f19 !important;
    border-top: 1px solid rgba(255, 255, 255, 0.1) !important;
  }
  .tl-text h2.tl-headline,
  .tl-text h2.tl-headline *,
  .tl-slide-content h2,
  .tl-slide-content h2 * {
    color: #ffffff !important;
  }
  .tl-text,
  .tl-text p,
  .tl-text *,
  .tl-slide-content p,
  .tl-slide-content * {
    color: #cbd5e1 !important;
  }
  .tl-timegroup-message {
    color: #a78bfa !important;
    font-weight: bold !important;
  }
  .tl-timemarker .tl-timemarker-content {
    background-color: #1e293b !important;
    border-color: #4f46e5 !important;
  }
  .tl-timemarker.tl-active .tl-timemarker-content {
    background-color: #6366f1 !important;
    border-color: #c084fc !important;
  }
  .tl-attribution {
    display: none !important; /* Hide attribution for clean UI */
  }

  /* Light Theme Overrides */
  .light-theme .tl-timeline {
    background-color: #f8fafc !important;
    color: #1e293b !important;
  }
  .light-theme .tl-slide {
    background-color: #f8fafc !important;
  }
  .light-theme .tl-navigation {
    background-color: #f1f5f9 !important;
    border-top: 1px solid rgba(0, 0, 0, 0.08) !important;
  }
  .light-theme .tl-text h2.tl-headline,
  .light-theme .tl-text h2.tl-headline *,
  .light-theme .tl-slide-content h2,
  .light-theme .tl-slide-content h2 * {
    color: #0f172a !important;
  }
  .light-theme .tl-text,
  .light-theme .tl-text p,
  .light-theme .tl-text *,
  .light-theme .tl-slide-content p,
  .light-theme .tl-slide-content * {
    color: #334155 !important;
  }
  .light-theme .tl-timegroup-message {
    color: #6d28d9 !important;
  }
  .light-theme .tl-timemarker .tl-timemarker-content {
    background-color: #e2e8f0 !important;
    border-color: #94a3b8 !important;
  }
  .light-theme .tl-timemarker.tl-active .tl-timemarker-content {
    background-color: #4f46e5 !important;
    border-color: #6d28d9 !important;
  }
</style>

<script>
  document.addEventListener("DOMContentLoaded", function() {
    window.timelineData = {
      "title": {
        "text": {
          "headline": "A Timeline of AI Milestones",
          "text": "Explore key breakthroughs in artificial intelligence, tracking theoretical milestones, scaling achievements, and practical agentic code systems."
        },
        "media": {
          "url": "/posts/images/ai_start.jpg",
          "caption": "Artificial intelligence digital brain visualization."
        }
      },
      "events": [
        {
          "start_date": { "year": "1956" },
          "text": {
            "headline": "1950s: Coining 'Artificial Intelligence'",
            "text": "The term 'artificial intelligence' is coined by computer scientists at the <a href='https://en.wikipedia.org/wiki/Dartmouth_workshop' target='_blank'>Dartmouth Workshop in 1956</a> hosted at <a href='https://maps.app.goo.gl/m4TNGqiGAwkvKibq6' target='_blank'>Dartmouth College</a>. This label was chosen in part to secure US military funding for early electromechanical computing research."
          },
          "media": {
            "url": "https://maps.app.goo.gl/m4TNGqiGAwkvKibq6",
            "caption": "Dartmouth College - Hanover, NH (Coining Location of AI)"
          }
        },
        {
          "start_date": { "year": "1966" },
          "text": {
            "headline": "1960s: ELIZA Chatbot",
            "text": "Joseph Weizenbaum creates <a href='https://en.wikipedia.org/wiki/ELIZA' target='_blank'>ELIZA at MIT</a>, an early chatbot simulating a therapist. It demonstrated human susceptibility to perceiving true intelligence, empathy, and intent in simple software responses."
          },
          "media": {
            "url": "/posts/images/eliza_terminal.png",
            "caption": "An example terminal session of ELIZA."
          }
        },
        {
          "start_date": { "year": "1997" },
          "text": {
            "headline": "1997: Deep Blue Defeats Kasparov",
            "text": "IBM's chess-playing computer <a href='https://en.wikipedia.org/wiki/Deep_Blue_(chess_computer)' target='_blank'>Deep Blue</a> defeats world chess champion Garry Kasparov in a historic six-game match, proving machine superiority in high-complexity strategic games."
          },
          "media": {
            "url": "/posts/images/deep_blue_kasparov.jpg",
            "caption": "Garry Kasparov playing Deep Blue in 1997."
          }
        },
        {
          "start_date": { "year": "2010" },
          "text": {
            "headline": "2010: DeepMind Founded",
            "text": "Demis Hassabis, Shane Legg, and Mustafa Suleyman found <a href='https://en.wikipedia.org/wiki/Google_DeepMind' target='_blank'>DeepMind Technologies</a> in London. Backed by early investor Elon Musk, they set out to solve intelligence using deep reinforcement learning."
          },
          "media": {
            "url": "/posts/images/ucl_portico.jpg",
            "caption": "University College London where Hassabis studied."
          }
        },
        {
          "start_date": { "year": "2011" },
          "text": {
            "headline": "2011: Google Brain Founded",
            "text": "Jeff Dean, Andrew Ng, and Greg Corrado establish <a href='https://en.wikipedia.org/wiki/Google_Brain' target='_blank'>Google Brain</a> to combine massive scale computing with deep neural networks."
          },
          "media": {
            "url": "/posts/images/google_logo.svg",
            "caption": "Google Brain deep neural network visualization."
          }
        },
        {
          "start_date": { "year": "2011", "month": "10" },
          "text": {
            "headline": "2011: Siri Voice Assistant",
            "text": "Apple integrates <a href='https://en.wikipedia.org/wiki/Siri' target='_blank'>Siri</a> into the iPhone 4S, showcasing highly effective voice recognition technology and introducing conversational assistants to millions of consumers."
          },
          "media": {
            "url": "/posts/images/iphone_siri.jpg",
            "caption": "Siri assistant on iPhone 4S."
          }
        },
        {
          "start_date": { "year": "2012" },
          "text": {
            "headline": "2012: The ImageNet Turning Point",
            "text": "The <a href='https://en.wikipedia.org/wiki/ImageNet' target='_blank'>ImageNet competition</a> marks a massive turning point for modern AI. Geoffrey Hinton, Alex Krizhevsky, and Ilya Sutskever apply deep neural network technology (AlexNet) to modern GPUs, achieving massive, unprecedented improvements in image recognition."
          },
          "media": {
            "url": "/posts/images/geoffrey_hinton.jpg",
            "caption": "Geoffrey Hinton, pioneer of deep neural networks."
          }
        },
        {
          "start_date": { "year": "2014" },
          "text": {
            "headline": "2014: Google Acquires DeepMind",
            "text": "Google <a href='https://en.wikipedia.org/wiki/Google_DeepMind#Acquisition_by_Google' target='_blank'>acquires DeepMind</a> for ~$500M. Elon Musk attempts a last-minute counter-bid to prevent Google (specifically co-founder Larry Page) from having unilateral control over AGI research, but DeepMind chose Google’s compute infrastructure."
          },
          "media": {
            "url": "/posts/images/google_london.jpg",
            "caption": "Google London offices."
          }
        },
        {
          "start_date": { "year": "2015" },
          "text": {
            "headline": "2015: OpenAI Founded",
            "text": "<a href='https://en.wikipedia.org/wiki/OpenAI' target='_blank'>OpenAI</a> is co-founded in San Francisco by Elon Musk, Sam Altman, Ilya Sutskever, Greg Brockman, and others as a non-profit laboratory with the mission to develop AI in the public sphere."
          },
          "media": {
            "url": "/posts/images/openai_logo.svg",
            "caption": "OpenAI research lab logo."
          }
        },
        {
          "start_date": { "year": "2016" },
          "text": {
            "headline": "2016: AlphaGo Defeats Lee Sedol",
            "text": "Google DeepMind's AlphaGo defeats 9-dan Go champion Lee Sedol 4-1 in Seoul, proving that deep reinforcement learning could conquer intuitive, high-complexity board games. Read details about the <a href='https://en.wikipedia.org/wiki/AlphaGo_versus_Lee_Sedol' target='_blank'>AlphaGo versus Lee Sedol match</a>."
          },
          "media": {
            "url": "https://www.youtube.com/watch?v=WXuK6gekU1Y",
            "caption": "AlphaGo - The Movie: award-winning documentary."
          }
        },
        {
          "start_date": { "year": "2017" },
          "text": {
            "headline": "2017: AlphaGo Beats Ke Jie",
            "text": "AlphaGo defeats world champion Ke Jie, handling Go's complex board game of Go with its vast number ($2 \\times 10^{170}$) of possible positions. Read details about the <a href='https://en.wikipedia.org/wiki/AlphaGo_versus_Ke_Jie' target='_blank'>AlphaGo versus Ke Jie match</a>."
          },
          "media": {
            "url": "/posts/images/ke_jie.jpg",
            "caption": "World Go Champion Ke Jie."
          }
        },
        {
          "start_date": { "year": "2017", "month": "6" },
          "text": {
            "headline": "2017: The Transformer Architecture",
            "text": "Google researchers publish <a href='https://arxiv.org/abs/1706.03762' target='_blank'>'Attention Is All You Need'</a>, introducing the self-attention based Transformer network architecture, which forms the foundation of all modern Large Language Models."
          },
          "media": {
            "url": "/posts/images/transformer_attention.png",
            "caption": "Self-attention mechanism representation."
          }
        },
        {
          "start_date": { "year": "2018" },
          "text": {
            "headline": "2018: GPT-1 & Musk's Departure",
            "text": "OpenAI releases <a href='https://en.wikipedia.org/wiki/Generative_pre-trained_transformer' target='_blank'>GPT-1</a> (117M parameters) to show unsupervised pre-training, and Elon Musk resigns from the board to avoid conflict with Tesla Autopilot. Releases from this era can be followed on the <a href='https://www.youtube.com/@OpenAI' target='_blank'>OpenAI YouTube Channel</a>."
          },
          "media": {
            "url": "/posts/images/openai_logo.svg",
            "caption": "OpenAI logo."
          }
        },
        {
          "start_date": { "year": "2019" },
          "text": {
            "headline": "2019: GPT-2 Release",
            "text": "OpenAI releases <a href='https://en.wikipedia.org/wiki/GPT-2' target='_blank'>GPT-2</a> (1.5B parameters), representing an early public iteration of large-scale, transformer-based language modeling. Explanations and demos are available on the <a href='https://www.youtube.com/@OpenAI' target='_blank'>OpenAI YouTube Channel</a>."
          },
          "media": {
            "url": "/posts/images/openai_office.jpg",
            "caption": "OpenAI offices."
          }
        },
        {
          "start_date": { "year": "2020", "month": "1" },
          "text": {
            "headline": "2020: The Neural Scaling Laws",
            "text": "OpenAI researchers publish the seminal <a href='https://arxiv.org/abs/2001.08361' target='_blank'>'Neural Scaling Laws' paper</a>, providing the mathematical and theoretical justification for the industry's focus on building larger and larger models."
          },
          "media": {
            "url": "/posts/images/neural_network_nodes.png",
            "caption": "Neural network node scaling model."
          }
        },
        {
          "start_date": { "year": "2020", "month": "6" },
          "text": {
            "headline": "2020-2021: GPT-3 Release",
            "text": "OpenAI releases <a href='https://en.wikipedia.org/wiki/GPT-3' target='_blank'>GPT-3</a>, featuring 175 billion parameters—a massive 100-fold increase in parameter size compared to GPT-2. Explore OpenAI's work on the <a href='https://www.youtube.com/@OpenAI' target='_blank'>OpenAI YouTube Channel</a>."
          },
          "media": {
            "url": "/posts/images/openai_logo.svg",
            "caption": "GPT-3 model architecture."
          }
        },
        {
          "start_date": { "year": "2020", "month": "11" },
          "text": {
            "headline": "November 2020: AlphaFold 2 Solves Protein Folding",
            "text": "Google DeepMind's <a href='https://en.wikipedia.org/wiki/AlphaFold' target='_blank'>AlphaFold 2</a> achieves breakthrough results at the CASP14 competition, solving the 50-year-old scientific grand challenge of protein folding. It stands as one of the most significant real-world scientific accomplishments in AI history, as highlighted in the documentary <a href='https://www.youtube.com/watch?v=d95J8yzvjbQ' target='_blank'>The Thinking Game</a>."
          },
          "media": {
            "url": "/posts/images/alphafold_protein.png",
            "caption": "AlphaFold 2 predicted protein structure."
          }
        },
        {
          "start_date": { "year": "2021" },
          "text": {
            "headline": "2021: GitHub Copilot Launches",
            "text": "GitHub and OpenAI introduce <a href='https://en.wikipedia.org/wiki/GitHub_Copilot' target='_blank'>Copilot Technical Preview</a>, powered by OpenAI Codex, establishing generative AI for developers directly in IDEs."
          },
          "media": {
            "url": "/posts/images/github_logo.jpg",
            "caption": "GitHub Copilot developer tool."
          }
        },
        {
          "start_date": { "year": "2022", "month": "11" },
          "text": {
            "headline": "Fall 2022: ChatGPT Launched",
            "text": "OpenAI releases <a href='https://en.wikipedia.org/wiki/ChatGPT' target='_blank'>ChatGPT</a> as a free public experiment. It rapidly gains 100 million active users in two months, becoming a massive 'technological trigger' for the current AI hype cycle. Check product announcements on the <a href='https://www.youtube.com/@OpenAI' target='_blank'>OpenAI YouTube Channel</a>."
          },
          "media": {
            "url": "/posts/images/chatgpt_logo.svg",
            "caption": "ChatGPT interface."
          }
        },
        {
          "start_date": { "year": "2023", "month": "3" },
          "text": {
            "headline": "March 2023: GPT-4 Launch",
            "text": "OpenAI launches <a href='https://en.wikipedia.org/wiki/GPT-4' target='_blank'>GPT-4</a>, establishing a new state of the art in professional and academic reasoning. It introduces a mixture-of-experts model totaling approximately one trillion parameters."
          },
          "media": {
            "url": "https://www.youtube.com/watch?v=outcGtwM2Dk",
            "caption": "The official GPT-4 Developer Livestream with Greg Brockman."
          }
        },
        {
          "start_date": { "year": "2023", "month": "12" },
          "text": {
            "headline": "December 2023: Google Releases Gemini 1.0",
            "text": "Google launches <a href='https://en.wikipedia.org/wiki/Gemini_(chatbot)' target='_blank'>Gemini 1.0</a>, its first natively multimodal model trained from the ground up on text, images, video, audio, and code. It is released in three sizes: Ultra, Pro, and Nano."
          },
          "media": {
            "url": "/posts/images/gemini_logo.jpg",
            "caption": "Google Gemini multimodal logo."
          }
        },
        {
          "start_date": { "year": "2024", "month": "2" },
          "text": {
            "headline": "February 2024: Google Launches Gemini 1.5 Pro",
            "text": "Google introduces <a href='https://en.wikipedia.org/wiki/Gemini_(chatbot)#Gemini_1.5' target='_blank'>Gemini 1.5 Pro</a>, featuring a breakthrough 1 million token context window. This massive context allows developers and users to process entire code repositories or hours of audio/video in a single prompt."
          },
          "media": {
            "url": "/posts/images/gemini_logo.jpg",
            "caption": "Google Gemini 1.5 architecture."
          }
        },
        {
          "start_date": { "year": "2024", "month": "5" },
          "text": {
            "headline": "May 2024: OpenAI Launches GPT-4o",
            "text": "OpenAI releases <a href='https://en.wikipedia.org/wiki/GPT-4o' target='_blank'>GPT-4o</a>, a natively multimodal model integrating text, vision, and audio processing in real time. This release accelerates the transition of consumer AI applications from static text to fluid, zero-latency voice interactions."
          },
          "media": {
            "url": "/posts/images/openai_logo.svg",
            "caption": "OpenAI multimodal logo."
          }
        },
        {
          "start_date": { "year": "2024", "month": "6" },
          "text": {
            "headline": "June 2024: Anthropic Claude 3.5 Sonnet",
            "text": "Anthropic releases <a href='https://en.wikipedia.org/wiki/Claude_(chatbot)' target='_blank'>Claude 3.5 Sonnet</a>, setting new industry standards for programming and reasoning. Along with the model, Anthropic introduces 'Artifacts,' providing users with a visual workspace to view and interact with code, SVGs, and documents."
          },
          "media": {
            "url": "/posts/images/claude_art.jpg",
            "caption": "Anthropic Claude interface."
          }
        },
        {
          "start_date": { "year": "2024", "month": "9" },
          "text": {
            "headline": "September 2024: OpenAI o1 (Strawberry)",
            "text": "OpenAI launches its <a href='https://en.wikipedia.org/wiki/OpenAI_o1' target='_blank'>o1 series</a> of reasoning models (codenamed Strawberry). Using reinforcement learning, o1 thinks and reasons step-by-step before generating responses, showing massive, unprecedented improvements on complex math, science, and coding benchmarks."
          },
          "media": {
            "url": "/posts/images/openai_logo.svg",
            "caption": "OpenAI reasoning models."
          }
        },
        {
          "start_date": { "year": "2024", "month": "12" },
          "text": {
            "headline": "December 2024: DeepSeek-V3 Release",
            "text": "Chinese firm DeepSeek launches <a href='https://en.wikipedia.org/wiki/DeepSeek' target='_blank'>DeepSeek-V3</a>, a 671-billion parameter Mixture-of-Experts (MoE) model. V3 matches the performance of leading closed-source models like GPT-4o, but at a fraction of their training and inference cost."
          },
          "media": {
            "url": "/posts/images/deepseek_revolution.jpg",
            "caption": "DeepSeek V3 model."
          }
        },
        {
          "start_date": { "year": "2025", "month": "1" },
          "text": {
            "headline": "January 2025: DeepSeek-R1 Open-Weights Reasoning",
            "text": "DeepSeek launches <a href='https://en.wikipedia.org/wiki/DeepSeek-R1' target='_blank'>DeepSeek-R1</a>, an open-weights reasoning model utilizing reinforcement learning to output step-by-step thinking. R1 matches OpenAI's o1 in complex math and coding, triggering a massive shockwave in tech valuations and validating the power of open-weights systems."
          },
          "media": {
            "url": "/posts/images/deepseek_revolution.jpg",
            "caption": "DeepSeek R1 reasoning architecture."
          }
        },
        {
          "start_date": { "year": "2025", "month": "2" },
          "text": {
            "headline": "February 2025: Claude 3.7 Sonnet & Claude Code",
            "text": "Anthropic introduces <a href='https://en.wikipedia.org/wiki/Claude_(chatbot)' target='_blank'>Claude 3.7 Sonnet</a>, the first hybrid reasoning model capable of toggling between standard and extended thinking modes. Along with it, they launch Claude Code, an agentic developer CLI tool that operates autonomously within codebases."
          },
          "media": {
            "url": "/posts/images/agentic_tools.jpg",
            "caption": "Agentic developer CLI tools."
          }
        },
        {
          "start_date": { "year": "2025", "month": "8" },
          "text": {
            "headline": "August 2025: GPT-5 & Scaling Plateau",
            "text": "GPT-5 is released by OpenAI, signaling a potential plateau or 'flop' as the industry faces clear diminishing returns on simply increasing model size, prompting a pivot to test-time compute and reinforcement-learning-driven reasoning. Launch event keynotes and demonstrations are available on the <a href='https://www.youtube.com/@OpenAI' target='_blank'>OpenAI YouTube Channel</a>."
          },
          "media": {
            "url": "/posts/images/gpt5_plateau.jpg",
            "caption": "The flattening scaling curve of LLMs."
          }
        }
      ]
    };
    
    window.timeline = new TL.Timeline('timeline-embed', JSON.parse(JSON.stringify(window.timelineData)), {
      font: 'default',
      is_embed: true
    });
  });
</script>

## 📖 Behind the History: A Personal Exploration

I was recently watching a great tech talk on YouTube—[Richard Campbell's keynote at NDC Copenhagen 2026, titled "After the AI Hype – What's Real, and What's Next"](https://www.youtube.com/watch?v=uWnUnMphmPM)—and it got me thinking deeply about the history of artificial intelligence to date. Having consumed content from multiple sources, I wanted to collate what I know so far and visually see how AI has exploded in the last couple of years. I wanted to visually see the milestones because I find this rapid evolution fascinating. 

Over the years, I've spent hours absorbing documentary films and literature on the topic. The cinematic, high-stakes story in [AlphaGo - The Movie](https://www.youtube.com/watch?v=WXuK6gekU1Y) and the scientific drama inside [The Thinking Game](https://www.youtube.com/watch?v=d95J8yzvjbQ) (which documents the quest to solve protein folding and AGI) both left a deep impression on me. I also consumed the book [*The Infinity Machine: Demis Hassabis, DeepMind, and the Quest for Superintelligence* by Sebastian Mallaby](https://www.goodreads.com/en/book/show/241434373-the-infinity-machine), which provides a brilliant, behind-the-scenes look at how DeepMind was built.

I have always been fascinated by Google DeepMind. Unlike many other players in the space who focus heavily on consumer text-generation applications, DeepMind approaches AI through a rigorous scientific lens—focusing on solving core mathematical and physical-world problems that directly benefit humanity.

The current AI landscape did not emerge overnight. It is the result of over a decade of fierce competition, massive infrastructure bets, paradigm-shifting scientific papers, and high-profile boardroom dramas. 

---

## 🎙️ A Reality Check: "After the AI Hype" (Richard Campbell, NDC 2026)

Before diving into the detailed history, it is crucial to ground ourselves. In his NDC Copenhagen 2026 keynote, **"After the AI Hype – What's Real, and What's Next"** (available on [YouTube](https://www.youtube.com/watch?v=uWnUnMphmPM)), tech veteran Richard Campbell provided a sobering reality check on these milestones:

*   **The "Artificial Intelligence" Name Problem (1950s):** Campbell highlights that the term "Artificial Intelligence" was originally coined in the 1950s (at the Dartmouth workshop in 1956) by John McCarthy and others primarily to secure military research funding. This sci-fi-infused Web naming has repeatedly fueled overinflated expectations, followed by crashing "AI Winters" when the technology failed to match the fiction.
*   **The CapEx Hype Cycle vs. Profitability:** Campbell compares the current generative AI investment boom to the late-1990s dot-com bubble. Tech giants are spending hundreds of billions on data centers and compute infrastructure, yet a sustainable, profitable business model for consumer LLMs has yet to be widely proven.
*   **Physical Breakthroughs vs. Pattern Recognition:** Amidst the marketing noise, Campbell identifies **DeepMind's AlphaFold** (detailed in [The Thinking Game](https://www.youtube.com/watch?v=d95J8yzvjbQ) documentary) as one of the only true, ground-level physical science breakthroughs. While LLMs excel at language pattern recognition, AlphaFold solved the 50-year-old protein folding problem, delivering tangible value to biology, chemistry, and drug discovery.
*   **The Developer's Focus:** His ultimate advice for developers: ignore the speculative market hype, and focus on applying these tools to solve real, practical, and concrete problems.

---

## 🔬 Era 1: The Deep Learning Renaissance (2010–2014)

The modern AI boom trace its roots back to the early 2010s, when researchers realized that deep neural networks, combined with GPU acceleration, could solve previously intractable pattern-recognition tasks.

### Early Milestones: Coining AI & ELIZA
While the modern explosion occurred in the 2010s, the conceptual roots go back decades:
*   **The 1950s:** The term "artificial intelligence" was first coined at the Dartmouth workshop in 1956. As keynote speaker Richard Campbell points out, this sci-fi-infused naming was chosen primarily to secure US military funding for electromechanical computing research.
*   **The 1960s:** Joseph Weizenbaum created ELIZA at MIT, an early chatbot that demonstrated how susceptible humans are to perceiving genuine intelligence, empathy, and intent in simple software responses.

### GPU Acceleration & Siri (2010–2012)
*   **Apple Siri (2011):** Apple integrated Siri into the iPhone 4S, bringing highly effective voice recognition technology and conversational interfaces to the global consumer market.
*   **The ImageNet Breakthrough (2012):** The ImageNet competition marked the ultimate turning point for deep learning. Geoffrey Hinton and his students applied deep neural networks (AlexNet) to modern GPUs, demonstrating massive, unprecedented improvements in image recognition and establishing deep learning as the dominant paradigm.

### The Rise of DeepMind
In **2010**, Demis Hassabis, Shane Legg, and Mustafa Suleyman founded **DeepMind Technologies** in London. Their goal was ambitious: to build general-purpose learning algorithms that could eventually lead to Artificial General Intelligence (AGI).

*   **Elon Musk's Investment:** In 2011/2012, Elon Musk became one of DeepMind's earliest angel investors (alongside Peter Thiel and Skype's Jaan Tallinn). Musk has publicly stated that his investment was not for financial return, but to keep a close eye on the speed of AI development and its existential risks.
*   **The Atari Breakthrough (2013):** DeepMind published a landmark paper showing a Deep Q-Network (DQN) that learned to play seven Atari 2600 games at superhuman levels directly from raw pixel inputs, without being programmed with the rules.
*   **The Acquisition (2014):** In early 2014, Google acquired DeepMind for approximately $500 million. Elon Musk reportedly attempted a last-minute, informal counter-bid to prevent Google (specifically co-founder Larry Page) from having unilateral control over AGI research, but DeepMind chose Google’s compute infrastructure.

### The Pioneers of Google Brain
Parallel to DeepMind's independent work in London, Google established **Google Brain** in **2011** as an internal research project. 

*   **Key Figures:** Co-founded by systems architect **Jeff Dean**, machine learning pioneer **Andrew Ng**, and Google researcher **Greg Corrado**. Later, researchers like **Quoc Le** joined, contributing major advances in sequence learning.
*   **The Cat Experiment (2012):** Google Brain made headlines by connecting 16,000 CPU cores to train a massive neural network on 10 million unlabeled YouTube video frames. The network autonomously learned to recognize high-level concepts, most famously self-identifying the face of a cat.

---

## 🚀 Era 2: The Birth of OpenAI & The Transformer (2015–2017)

As Google's control over AI research consolidated via DeepMind and Google Brain, fears grew that a single corporation would monopolize the benefits of AGI.

```
       [Google Brain (2011)]        [DeepMind (2010)]
                 │                         │
                 ▼                         ▼
         [Cat Video (2012)]        [Atari DQN (2013)]
                 │                         │
                 └───────────┬─────────────┘
                             │ (Google acquires DeepMind, 2014)
                             ▼
                 [OpenAI Founded (Dec 2015)]
              (Co-funded by Elon Musk & Sam Altman
                as a non-profit counterweight)
```

### The Genesis of OpenAI
In **December 2015**, **OpenAI** was founded in San Francisco as a non-profit research laboratory. 

*   **The Founders:** Elon Musk, Sam Altman, Greg Brockman, Ilya Sutskever (lured away from Google Brain), Wojciech Zaremba, and John Schulman, with a collective pledge of $1 billion from Musk, Reid Hoffman, Peter Thiel, and others.
*   **Musk's Motivation:** Musk co-chaired the board and was the primary initial financial donor. He envisioned OpenAI as a transparent, open-source counterweight to Google, developing AI safely and distributing its benefits democratically.
*   **DeepMind's AlphaGo (2016):** While OpenAI was organizing, Google DeepMind achieved one of the most celebrated milestones in computer science. In March 2016, **AlphaGo** defeated Lee Sedol, a 9-dan Go champion, 4 games to 1 in Seoul, South Korea. The match proved that deep reinforcement learning could conquer intuitive, high-complexity board games cavities ahead of expert predictions.

### The Invention of the Transformer (2017)
In June 2017, a team of eight Google researchers published the seminal paper: **"Attention Is All You Need"**. 

The paper introduced the **Transformer** architecture, which replaced recurrent neural networks (RNNs) with self-attention mechanisms. The Transformer allowed for massive parallelization during training, unlocking the ability to train neural networks on web-scale datasets. This single architectural breakthrough forms the foundation of every modern Large Language Model (LLM).

---

## 📈 Era 3: The Scaling Era & Developer Tools (2018–2021)

With the Transformer in hand, the race to scale models began. This era saw OpenAI shift its business model and release the first three generations of the Generative Pre-trained Transformer (GPT).

### The Departure of Elon Musk & Capped-Profit Transition
*   **Musk Steps Down (2018):** In February 2018, Elon Musk resigned from OpenAI's board. The official reason was to avoid conflicts of interest with Tesla’s Autopilot computer vision development. However, later reports revealed tension: Musk had proposed taking control of OpenAI to accelerate its pace, which was rejected by Altman and Brockman.
*   **OpenAI LP (2019):** Realizing that training state-of-the-art models required billions of dollars in cloud compute, OpenAI transitioned from a pure non-profit to a "capped-profit" model (OpenAI LP). This allowed them to secure a **$1 billion investment from Microsoft** in July 2019, providing the massive Azure infrastructure needed for scaling.

### The GPT Release Timeline
*   **GPT-1 (June 2018):** OpenAI published *"Improving Language Understanding by Generative Pre-Training"*, introducing GPT-1. It had **117 million parameters** and proved that unsupervised pre-training followed by supervised fine-tuning was highly effective.
*   **GPT-2 (February 2019):** scaled up to **1.5 billion parameters**. OpenAI initially declared GPT-2 "too dangerous to release" due to concerns over automated misinformation generation, sparking intense public debate. It was eventually released in full in November 2019.
*   **Neural Scaling Laws (2020):** OpenAI researchers published a landmark paper on scaling laws for neural language models, providing the mathematical and theoretical justification for the industry's focus on building larger and larger models.
*   **GPT-3 (June 2020):** A massive leap to **175 billion parameters**. GPT-3 demonstrated remarkable in-context learning—allowing it to write code, compose poetry, and perform translations with zero or few examples, without weight updates.

### Scientific Grand Challenge: AlphaFold 2 (2020)
While large language models were scaling parameters, Google DeepMind achieved one of the most celebrated milestones in real-world physical science. In **November 2020**, DeepMind's **AlphaFold 2** solved the 50-year-old protein folding problem at the CASP14 competition, predicting 3D structures of proteins with atomic accuracy. This breakthrough, documented in the film *The Thinking Game*, showed that AI could act as a scientific engine to solve complex biological challenges for the direct benefit of humanity.

### The Dawn of GitHub Copilot (2021)
In June 2021, GitHub (partnering with OpenAI) announced the Technical Preview of **GitHub Copilot**. Powered by **OpenAI Codex** (a GPT-3 model fine-tuned on public source code), Copilot brought generative AI directly into developers' code editors. It represented the first massive commercial success of generative AI as an everyday utility.

---

## 💥 Era 4: The ChatGPT Explosion & LLM Wars (2022–2023)

By late 2022, AI moved from a developer utility and academic curiosity into the global mainstream.

### ChatGPT (November 30, 2022)
OpenAI quietly released **ChatGPT**, a free research preview based on a fine-tuned version of GPT-3.5 (InstructGPT) using Reinforcement Learning from Human Feedback (RLHF). ChatGPT’s conversational interface became an overnight sensation, reaching **100 million monthly active users in two months**, making it the fastest-growing consumer application in history.

### GPT-4 (March 14, 2023)
OpenAI followed its success with the release of **GPT-4**. Showing human-level performance on professional and academic tests (like passing the Uniform Bar Exam in the 90th percentile), GPT-4 established a new state-of-the-art benchmark for reasoning and coding.

### The Rise of Competitors
*   **Anthropic & Claude:** Founded in 2021 by Dario and Daniela Amodei (former OpenAI VP of Research and VP of Safety, who left due to disagreements over OpenAI's commercial direction and Microsoft partnership). Anthropic released **Claude 1** in March 2023 and **Claude 2** in July 2023, emphasizing "Constitutional AI" for safety.
*   **Google's Response:** Stunned by ChatGPT's launch, Google declared a "code red" and launched **Bard** in early 2023. In April 2023, Google officially merged **Google Brain** and **DeepMind** into **Google DeepMind** under Demis Hassabis's leadership to consolidate its AI efforts.
*   **Meta's LLaMA (February 2023):** Meta released its LLaMA model weights to researchers, which leaked shortly after. This accidental leak sparked the open-weights/open-source AI revolution, leading to a massive community-driven ecosystem of localized, fine-tuned models.
*   **Elon Musk's xAI:** In July 2023, Elon Musk returned to the LLM race by launching **xAI**. In November 2023, xAI introduced **Grok**, an AI assistant integrated directly into X (formerly Twitter) with real-time access to social media feeds.

---

## 🤖 Era 5: Multimodality, Scaling Plateaus, & Agents (2024–2026)

The current era has transitioned from static text generation to native multimodal processing, autonomous coding agents, and a fundamental shift from parameter scaling to inference-time reasoning.

### Multimodal Breakthroughs & Model Releases (2024)
*   **Natively Multimodal Models (May 2024):** OpenAI launched **GPT-4o**, integrating text, vision, and audio processing in real time. 
*   **Claude 3.5 Sonnet (June 2024):** Anthropic released Claude 3.5 Sonnet, setting new state-of-the-art benchmarks in coding and reasoning, and introduced the interactive "Artifacts" panel.
*   **OpenAI o1 Reasoning (September 2024):** OpenAI launched the o1 series, using reinforcement learning to execute step-by-step reasoning at inference time.
*   **DeepSeek-V3 (December 2024):** DeepSeek launched V3, a 671-billion parameter Mixture-of-Experts model, matching top closed models at a fraction of training cost.

### Reasoning Models & Agentic Ecosystem (2025–2026)
*   **DeepSeek-R1 (January 2025):** DeepSeek released R1, an open-weights reasoning model matching closed reasoning models in STEM and coding benchmarks.
*   **Claude 3.7 Sonnet & Claude Code (February 2025):** Anthropic introduced Claude 3.7 Sonnet, the first hybrid reasoning model, alongside Claude Code, an autonomous CLI agent operating directly in code repositories.
*   **GPT-5 & The Scaling Plateau (August 2025):** OpenAI released GPT-5, highlighting a pre-training scaling plateau where increasing parameters yielded diminishing returns, confirming the shift toward inference-time compute.
*   **Recent 2026 Innovations:** 2026 has seen the release of next-generation models like **Anthropic Claude Opus 4.8 / Sonnet 5**, **OpenAI GPT-5.5**, and **Google Gemini 3.1 Pro**, representing highly specialized, agentic, and low-latency workflows operating across professional developer environments.

---

## 🚶‍♂️ The AI Milestone Walker

To help you walk through the timeline and inspect individual markers clearly, use the interactive Walker tool below. You can search for specific breakthroughs, filter by categories, or run the auto-play slideshow.

<div id="milestone-walker" class="mw-container">
  <!-- Top bar with search and filters -->
  <div class="mw-header">
    <div class="mw-header-left">
      <h3 class="mw-title">Milestone Explorer</h3>
      <p class="mw-subtitle">Walk through key moments in artificial intelligence history step-by-step</p>
    </div>
    <div class="mw-header-right">
      <div class="mw-search-wrapper">
        <span class="mw-search-icon">🔍</span>
        <input type="text" id="mw-search" placeholder="Search milestones..." aria-label="Search milestones">
      </div>
      <div class="mw-autoplay-controls">
        <button id="mw-btn-play" class="mw-btn mw-btn-icon" title="Auto-play Slideshow">
          <span class="mw-play-icon">▶</span>
          <span class="mw-pause-icon" style="display:none;">⏸</span>
        </button>
        <select id="mw-autoplay-speed" class="mw-select" title="Autoplay interval" aria-label="Autoplay interval">
          <option value="3000">3s</option>
          <option value="5000" selected>5s</option>
          <option value="8000">8s</option>
        </select>
      </div>
    </div>
  </div>

  <!-- Filter Pills -->
  <div class="mw-filters-container">
    <span class="mw-filter-label">Filter:</span>
    <div class="mw-filter-pills" id="mw-filter-pills">
      <button class="mw-pill active" data-category="all">All Milestones</button>
      <button class="mw-pill" data-category="pioneering">Classic AI (Pre-2018)</button>
      <button class="mw-pill" data-category="deepmind">Google & DeepMind</button>
      <button class="mw-pill" data-category="openai">OpenAI & GPT</button>
      <button class="mw-pill" data-category="opensource">Open Weights & Others</button>
    </div>
  </div>

  <!-- Markers Track wrapper -->
  <div class="mw-track-wrapper">
    <button class="mw-track-scroll-btn mw-left" id="mw-scroll-left" title="Scroll Left">◀</button>
    <div class="mw-track-scroll-area" id="mw-track-scroll-area">
      <div class="mw-track-content" id="mw-track-content">
        <div class="mw-track-line-bg" id="mw-track-line-bg"></div>
        <div class="mw-track-line-progress" id="mw-track-line-progress"></div>
        <div class="mw-markers-row" id="mw-markers-row">
          <!-- Marker nodes will be dynamically generated here -->
        </div>
      </div>
    </div>
    <button class="mw-track-scroll-btn mw-right" id="mw-scroll-right" title="Scroll Right">▶</button>
  </div>

  <!-- Main Card Display -->
  <div class="mw-card" id="mw-card">
    <div class="mw-card-content">
      <div class="mw-card-info">
        <div class="mw-card-meta">
          <span class="mw-badge mw-badge-date" id="mw-card-date">1956</span>
          <span class="mw-badge mw-badge-category" id="mw-card-category">Pioneering</span>
        </div>
        <h4 class="mw-card-title" id="mw-card-title">Coining 'Artificial Intelligence'</h4>
        <div class="mw-card-description" id="mw-card-desc">
          <!-- Text content -->
        </div>
      </div>
      <div class="mw-card-visual">
        <div class="mw-media-container" id="mw-media-container">
          <!-- Image or Iframe goes here -->
        </div>
      </div>
    </div>
  </div>

  <!-- Footer controls -->
  <div class="mw-footer">
    <div class="mw-progress-info">
      <span id="mw-current-index">1</span> of <span id="mw-total-count">29</span> milestones
    </div>
    <div class="mw-nav-buttons">
      <button id="mw-btn-prev" class="mw-btn mw-btn-secondary" disabled>◀ Previous</button>
      <button id="mw-btn-next" class="mw-btn mw-btn-primary">Next ▶</button>
    </div>
  </div>
</div>

<style>
/* Milestone Walker styles */
.mw-container {
  background: linear-gradient(135deg, #1e293b, #0f172a);
  border: 1px solid rgba(255, 255, 255, 0.08);
  border-radius: 16px;
  padding: 2rem;
  margin: 3rem 0;
  box-shadow: 0 10px 30px -10px rgba(0, 0, 0, 0.5);
  font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif;
  color: #e2e8f0;
  transition: all 0.3s ease;
}

body.light-theme .mw-container {
  background: linear-gradient(135deg, #ffffff, #f8fafc);
  border: 1px solid rgba(0, 0, 0, 0.08);
  box-shadow: 0 10px 30px -10px rgba(0, 0, 0, 0.05);
  color: #334155;
}

.mw-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  flex-wrap: wrap;
  gap: 1.5rem;
  margin-bottom: 1.5rem;
  border-bottom: 1px solid rgba(255, 255, 255, 0.08);
  padding-bottom: 1.5rem;
}

body.light-theme .mw-header {
  border-bottom-color: rgba(0, 0, 0, 0.08);
}

.mw-header-left h3.mw-title {
  margin: 0 0 0.25rem 0 !important;
  font-size: 1.75rem !important;
  font-weight: 800 !important;
  background: linear-gradient(135deg, #c084fc, #6366f1);
  -webkit-background-clip: text !important;
  -webkit-text-fill-color: transparent !important;
  border: none !important;
}

.mw-subtitle {
  margin: 0 !important;
  font-size: 0.95rem;
  color: #94a3b8;
}

body.light-theme .mw-subtitle {
  color: #64748b;
}

.mw-header-right {
  display: flex;
  align-items: center;
  gap: 1rem;
  flex-wrap: wrap;
}

.mw-search-wrapper {
  position: relative;
  display: flex;
  align-items: center;
}

.mw-search-icon {
  position: absolute;
  left: 0.75rem;
  color: #64748b;
  pointer-events: none;
}

#mw-search {
  padding: 0.5rem 1rem 0.5rem 2.2rem !important;
  background: rgba(15, 23, 42, 0.6) !important;
  border: 1px solid rgba(255, 255, 255, 0.1) !important;
  border-radius: 9999px !important;
  color: #f8fafc !important;
  font-size: 0.9rem !important;
  width: 220px !important;
  transition: all 0.2s ease !important;
  height: auto !important;
}

body.light-theme #mw-search {
  background: #ffffff !important;
  border: 1px solid #cbd5e1 !important;
  color: #0f172a !important;
}

#mw-search:focus {
  border-color: #6366f1 !important;
  box-shadow: 0 0 0 3px rgba(99, 102, 241, 0.2) !important;
  width: 260px !important;
}

.mw-autoplay-controls {
  display: flex;
  align-items: center;
  gap: 0.5rem;
}

.mw-btn {
  display: inline-flex;
  align-items: center;
  justify-content: center;
  padding: 0.5rem 1rem;
  border-radius: 9999px;
  font-weight: 600;
  font-size: 0.9rem;
  cursor: pointer;
  transition: all 0.2s ease;
  border: none;
}

.mw-btn-icon {
  width: 2.2rem;
  height: 2.2rem;
  padding: 0;
  border-radius: 50%;
  background: rgba(99, 102, 241, 0.1);
  color: #a5b4fc;
}

.mw-btn-icon:hover {
  background: #6366f1;
  color: white;
}

.mw-select {
  padding: 0.35rem 1.8rem 0.35rem 0.75rem !important;
  background: rgba(15, 23, 42, 0.6) !important;
  border: 1px solid rgba(255, 255, 255, 0.1) !important;
  border-radius: 8px !important;
  color: #e2e8f0 !important;
  font-size: 0.85rem !important;
  cursor: pointer !important;
  height: auto !important;
  width: auto !important;
}

body.light-theme .mw-select {
  background: #ffffff !important;
  border: 1px solid #cbd5e1 !important;
  color: #334155 !important;
}

.mw-filters-container {
  display: flex;
  align-items: center;
  gap: 0.75rem;
  margin-bottom: 1.5rem;
}

.mw-filter-label {
  font-size: 0.85rem;
  font-weight: bold;
  color: #64748b;
  text-transform: uppercase;
  letter-spacing: 0.05em;
}

.mw-filter-pills {
  display: flex;
  gap: 0.5rem;
  flex-wrap: wrap;
}

.mw-pill {
  padding: 0.35rem 0.85rem !important;
  border-radius: 9999px !important;
  font-size: 0.8rem !important;
  font-weight: 600 !important;
  cursor: pointer !important;
  background: rgba(255, 255, 255, 0.04) !important;
  border: 1px solid rgba(255, 255, 255, 0.08) !important;
  color: #94a3b8 !important;
  transition: all 0.2s ease !important;
  display: inline-block !important;
}

body.light-theme .mw-pill {
  background: #f1f5f9 !important;
  border: 1px solid #cbd5e1 !important;
  color: #64748b !important;
}

.mw-pill:hover {
  background: rgba(255, 255, 255, 0.08) !important;
  color: #f8fafc !important;
}

body.light-theme .mw-pill:hover {
  background: #e2e8f0 !important;
  color: #0f172a !important;
}

.mw-pill.active {
  background: linear-gradient(135deg, #6366f1, #a855f7) !important;
  border-color: transparent !important;
  color: white !important;
  box-shadow: 0 4px 12px rgba(99, 102, 241, 0.3) !important;
}

/* Markers Track styles */
.mw-track-wrapper {
  position: relative;
  display: flex;
  align-items: center;
  margin: 2rem 0;
  background: rgba(15, 23, 42, 0.3);
  padding: 1rem 0.5rem;
  border-radius: 12px;
  border: 1px solid rgba(255, 255, 255, 0.04);
}

body.light-theme .mw-track-wrapper {
  background: rgba(241, 245, 249, 0.5);
  border-color: rgba(0, 0, 0, 0.04);
}

.mw-track-scroll-btn {
  background: transparent;
  border: none;
  color: #64748b;
  cursor: pointer;
  padding: 0.5rem;
  font-size: 1rem;
  transition: color 0.2s;
  z-index: 5;
}

.mw-track-scroll-btn:hover {
  color: #6366f1;
}

.mw-track-scroll-area {
  flex: 1;
  overflow-x: auto;
  overflow-y: hidden;
  position: relative;
  padding: 0 1rem 12px 1rem;
  scrollbar-width: none;
  height: 80px; /* Fixed height for scroll area container */
  display: flex;
  align-items: flex-end; /* Rest the content wrapper at the bottom */
  box-sizing: border-box;
}

.mw-track-scroll-area::-webkit-scrollbar {
  display: none;
}

.mw-track-content {
  position: relative;
  width: max-content;
  height: 56px;
  display: flex;
  align-items: flex-end;
}

.mw-track-line-bg {
  position: absolute;
  height: 4px;
  background: rgba(255, 255, 255, 0.08);
  border-radius: 2px;
  bottom: 4px; /* Align precisely with the center of the dots (6px center) */
  z-index: 1;
  pointer-events: none; /* Ignore pointer events so dots can be clicked */
}

body.light-theme .mw-track-line-bg {
  background: rgba(0, 0, 0, 0.08);
}

.mw-track-line-progress {
  position: absolute;
  height: 4px;
  background: linear-gradient(90deg, #6366f1, #a855f7);
  border-radius: 2px;
  bottom: 4px; /* Align precisely with the center of the active dot */
  z-index: 2;
  transition: width 0.3s cubic-bezier(0.4, 0, 0.2, 1), left 0.3s cubic-bezier(0.4, 0, 0.2, 1);
  pointer-events: none; /* Ignore pointer events so dots can be clicked */
}

.mw-markers-row {
  display: flex;
  gap: 3.5rem;
  position: relative;
  z-index: 3;
  width: max-content;
  padding: 0 2rem;
  height: 56px; /* Row height matching text + dot height */
  align-items: flex-end; /* resting dots at the bottom */
}

.mw-marker-node {
  display: flex;
  flex-direction: column;
  align-items: center;
  cursor: pointer;
  user-select: none;
  position: relative;
  height: 56px;
  justify-content: flex-end;
}

.mw-marker-year {
  font-size: 0.75rem;
  font-weight: 800;
  color: #64748b;
  margin-bottom: 8px; /* Safe padding between year text and dot */
  transition: all 0.2s ease;
  white-space: nowrap;
  display: block;
}

.mw-marker-dot {
  width: 12px;
  height: 12px;
  border-radius: 50%;
  background: #334155;
  border: 2px solid #0f172a;
  transition: all 0.2s cubic-bezier(0.34, 1.56, 0.64, 1);
  box-shadow: 0 0 0 2px rgba(255, 255, 255, 0.05);
}

body.light-theme .mw-marker-dot {
  background: #cbd5e1;
  border-color: #ffffff;
  box-shadow: 0 0 0 2px rgba(0, 0, 0, 0.05);
}

.mw-marker-node:hover .mw-marker-dot {
  background: #6366f1;
  transform: scale(1.3);
  box-shadow: 0 0 8px rgba(99, 102, 241, 0.8);
}

.mw-marker-node:hover .mw-marker-year {
  color: #e2e8f0;
}

body.light-theme .mw-marker-node:hover .mw-marker-year {
  color: #0f172a;
}

.mw-marker-node.active .mw-marker-dot {
  background: #ffffff;
  border-color: #6366f1;
  transform: scale(1.5);
  box-shadow: 0 0 12px 3px rgba(99, 102, 241, 0.6);
  width: 14px;
  height: 14px;
}

body.light-theme .mw-marker-node.active .mw-marker-dot {
  background: #ffffff;
  border-color: #4f46e5;
  box-shadow: 0 0 12px 3px rgba(79, 70, 229, 0.4);
}

.mw-marker-node.active .mw-marker-year {
  color: #a5b4fc;
  font-size: 0.85rem;
}

body.light-theme .mw-marker-node.active .mw-marker-year {
  color: #4f46e5;
}

/* Card details styling */
.mw-card {
  background: rgba(15, 23, 42, 0.4);
  border: 1px solid rgba(255, 255, 255, 0.06);
  border-radius: 12px;
  padding: 1.5rem;
  margin-top: 1.5rem;
  height: 410px; /* Fixed height on desktop to prevent layout shifting */
  transition: all 0.3s ease;
  position: relative;
  overflow: hidden;
}

body.light-theme .mw-card {
  background: #ffffff;
  border-color: rgba(0, 0, 0, 0.06);
  box-shadow: inset 0 0 0 1px rgba(0, 0, 0, 0.02), 0 4px 20px rgba(0, 0, 0, 0.02);
}

.mw-card-content {
  display: grid;
  grid-template-columns: 1.2fr 1fr;
  gap: 2rem;
  height: 100%;
}

@media (max-width: 768px) {
  .mw-card-content {
    grid-template-columns: 1fr;
  }
  .mw-card {
    height: auto;
    min-height: 520px; /* Standardize mobile min-height to reduce jumps */
  }
}

.mw-card-info {
  display: flex;
  flex-direction: column;
  justify-content: flex-start; /* Align to top */
  height: 100%;
  overflow: hidden;
  transition: opacity 0.3s ease, transform 0.3s ease;
}

.mw-card-meta {
  display: flex;
  gap: 0.5rem;
  margin-bottom: 1rem;
}

.mw-badge {
  font-size: 0.75rem;
  font-weight: 700;
  padding: 0.25rem 0.75rem;
  border-radius: 4px;
  text-transform: uppercase;
  letter-spacing: 0.05em;
}

.mw-badge-date {
  background: rgba(255, 255, 255, 0.1);
  color: #ffffff;
}

body.light-theme .mw-badge-date {
  background: rgba(0, 0, 0, 0.08);
  color: #0f172a;
}

.mw-badge-category {
  background: rgba(99, 102, 241, 0.2);
  color: #a5b4fc;
}

body.light-theme .mw-badge-category {
  background: rgba(79, 70, 229, 0.1);
  color: #4f46e5;
}

.mw-card-title {
  font-size: 1.6rem !important;
  font-weight: 800 !important;
  margin: 0 0 1rem 0 !important;
  line-height: 1.25 !important;
  color: #ffffff !important;
  border-bottom: none !important;
  transition: color 0.3s ease;
}

body.light-theme .mw-card-title {
  color: #0f172a !important;
}

.mw-card-description {
  font-size: 0.95rem;
  line-height: 1.6;
  color: #cbd5e1;
  overflow-y: auto; /* Scroll if description overflows */
  padding-right: 0.5rem;
  flex: 1;
}

/* Custom premium scrollbar for description */
.mw-card-description::-webkit-scrollbar {
  width: 4px;
}
.mw-card-description::-webkit-scrollbar-track {
  background: transparent;
}
.mw-card-description::-webkit-scrollbar-thumb {
  background: rgba(99, 102, 241, 0.3);
  border-radius: 2px;
}
.mw-card-description::-webkit-scrollbar-thumb:hover {
  background: rgba(99, 102, 241, 0.6);
}

body.light-theme .mw-card-description {
  color: #475569;
}

.mw-card-description a {
  color: #a5b4fc;
  font-weight: bold;
  text-decoration: underline;
  transition: color 0.2s;
}

body.light-theme .mw-card-description a {
  color: #4f46e5;
}

.mw-card-description a:hover {
  color: #c084fc;
}

body.light-theme .mw-card-description a:hover {
  color: #6366f1;
}

.mw-card-visual {
  display: flex;
  align-items: center;
  justify-content: center;
  border-radius: 8px;
  overflow: hidden;
  background: #0b0f19;
  border: 1px solid rgba(255, 255, 255, 0.05);
  position: relative;
  height: 100%;
}

body.light-theme .mw-card-visual {
  background: #f1f5f9;
  border-color: rgba(0, 0, 0, 0.05);
}

.mw-media-container {
  width: 100%;
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
  position: relative;
}

.mw-img {
  width: 100%;
  flex: 1;
  min-height: 0;
  object-fit: cover;
  transition: transform 0.5s ease;
}

.mw-img:hover {
  transform: scale(1.05);
}

.mw-iframe {
  width: 100%;
  flex: 1;
  min-height: 0;
  border: none;
}

/* Custom Location Card for Google Maps */
.mw-maps-card {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 2rem;
  text-align: center;
  background: radial-gradient(circle at center, #1e1e38 0%, #0b0f19 100%);
  width: 100%;
  height: 100%;
  color: #f8fafc;
}

body.light-theme .mw-maps-card {
  background: radial-gradient(circle at center, #ffffff 0%, #e2e8f0 100%);
  color: #0f172a;
}

.mw-maps-icon {
  font-size: 3rem;
  margin-bottom: 1rem;
  filter: drop-shadow(0 0 10px rgba(99, 102, 241, 0.6));
}

.mw-maps-caption {
  font-size: 0.9rem;
  margin-bottom: 1.5rem;
  color: #94a3b8;
  max-width: 80%;
}

body.light-theme .mw-maps-caption {
  color: #64748b;
}

.mw-maps-btn {
  background: linear-gradient(135deg, #6366f1, #a855f7);
  color: white;
  border: none;
  border-radius: 6px;
  padding: 0.6rem 1.2rem;
  font-weight: 600;
  font-size: 0.85rem;
  cursor: pointer;
  box-shadow: 0 4px 12px rgba(99, 102, 241, 0.3);
  transition: all 0.2s;
  text-decoration: none !important;
}

.mw-maps-btn:hover {
  transform: translateY(-1px);
  box-shadow: 0 6px 15px rgba(99, 102, 241, 0.4);
}

/* Footer Control styles */
.mw-footer {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-top: 1.5rem;
  border-top: 1px solid rgba(255, 255, 255, 0.08);
  padding-top: 1.5rem;
}

body.light-theme .mw-footer {
  border-top-color: rgba(0, 0, 0, 0.08);
}

.mw-progress-info {
  font-size: 0.9rem;
  color: #94a3b8;
  font-weight: 600;
}

body.light-theme .mw-progress-info {
  color: #64748b;
}

.mw-nav-buttons {
  display: flex;
  gap: 0.5rem;
}

.mw-btn-primary {
  background: linear-gradient(135deg, #6366f1, #a855f7);
  color: white;
  box-shadow: 0 4px 10px rgba(99, 102, 241, 0.2);
}

.mw-btn-primary:hover:not(:disabled) {
  transform: translateY(-1px);
  box-shadow: 0 6px 15px rgba(99, 102, 241, 0.3);
}

.mw-btn-secondary {
  background: rgba(255, 255, 255, 0.06);
  border: 1px solid rgba(255, 255, 255, 0.1);
  color: #cbd5e1;
}

body.light-theme .mw-btn-secondary {
  background: #f1f5f9;
  border-color: #cbd5e1;
  color: #475569;
}

.mw-btn-secondary:hover:not(:disabled) {
  background: rgba(255, 255, 255, 0.1);
  color: white;
}

body.light-theme .mw-btn-secondary:hover:not(:disabled) {
  background: #e2e8f0;
  color: #0f172a;
}

.mw-btn:disabled {
  opacity: 0.4;
  cursor: not-allowed;
  transform: none !important;
  box-shadow: none !important;
}

.mw-empty-state {
  text-align: center;
  padding: 3rem 1rem;
  color: #64748b;
  width: 100%;
}

.mw-empty-icon {
  font-size: 2.5rem;
  margin-bottom: 0.75rem;
}
</style>

<script>
  document.addEventListener("DOMContentLoaded", function() {
    // Wait for window.timelineData to be loaded
    const checkTimelineData = setInterval(() => {
      if (window.timelineData && window.timelineData.events) {
        clearInterval(checkTimelineData);
        initMilestoneWalker();
      }
    }, 100);

    function initMilestoneWalker() {
      const events = JSON.parse(JSON.stringify(window.timelineData.events));
      
      // Parse categories and store original indices
      events.forEach((event, index) => {
        event.originalIndex = index;
        const cat = getEventCategory(event, index);
        event.category = cat.id;
        event.categoryName = cat.name;
      });
      
      let filteredEvents = [...events];
      let currentIndex = 0;
      let autoplayInterval = null;
      
      const markersRow = document.getElementById("mw-markers-row");
      const progressLine = document.getElementById("mw-track-line-progress");
      const trackScrollArea = document.getElementById("mw-track-scroll-area");
      const cardTitle = document.getElementById("mw-card-title");
      const cardDesc = document.getElementById("mw-card-desc");
      const cardDate = document.getElementById("mw-card-date");
      const cardCategory = document.getElementById("mw-card-category");
      const mediaContainer = document.getElementById("mw-media-container");
      const currentIndexEl = document.getElementById("mw-current-index");
      const totalCountEl = document.getElementById("mw-total-count");
      const btnPrev = document.getElementById("mw-btn-prev");
      const btnNext = document.getElementById("mw-btn-next");
      const btnPlay = document.getElementById("mw-btn-play");
      const playIcon = btnPlay.querySelector(".mw-play-icon");
      const pauseIcon = btnPlay.querySelector(".mw-pause-icon");
      const autoplaySpeedSelect = document.getElementById("mw-autoplay-speed");
      const searchInput = document.getElementById("mw-search");
      const filterPills = document.getElementById("mw-filter-pills");
      
      // Category mapping logic
      function getEventCategory(event, index) {
        const headline = event.text.headline.toLowerCase();
        const year = parseInt(event.start_date.year);
        
        if (headline.includes('deepmind') || headline.includes('alphago') || headline.includes('alphafold') || headline.includes('gemini') || headline.includes('google brain') || headline.includes('transformer')) {
          return { id: 'deepmind', name: 'Google & DeepMind' };
        } else if (headline.includes('openai') || headline.includes('gpt') || headline.includes('copilot') || headline.includes('chatgpt')) {
          return { id: 'openai', name: 'OpenAI & GPT' };
        } else if (headline.includes('deepseek') || headline.includes('claude') || headline.includes('anthropic') || headline.includes('meta') || headline.includes('llama')) {
          return { id: 'opensource', name: 'Open Weights & Others' };
        } else {
          if (year < 2018) {
            return { id: 'pioneering', name: 'Classic AI' };
          }
          return { id: 'opensource', name: 'Open Weights & Others' };
        }
      }
      
      function renderMarkers() {
        markersRow.innerHTML = "";
        if (filteredEvents.length === 0) {
          markersRow.innerHTML = `
            <div class="mw-empty-state">
              <div class="mw-empty-icon">🔍</div>
              <div>No milestones match your search filters</div>
            </div>
          `;
          progressLine.style.width = "0%";
          return;
        }
        
        filteredEvents.forEach((event, idx) => {
          const node = document.createElement("div");
          node.className = `mw-marker-node ${idx === currentIndex ? 'active' : ''}`;
          node.dataset.index = idx;
          
          const year = event.start_date.year;
          const month = event.start_date.month ? `/${event.start_date.month}` : '';
          
          node.innerHTML = `
            <span class="mw-marker-year">${year}${month}</span>
            <div class="mw-marker-dot"></div>
          `;
          
          node.addEventListener("click", () => {
            selectMilestone(idx);
            pauseAutoplay();
          });
          
          markersRow.appendChild(node);
        });
        
        updateProgressLine();
      }
      
      function updateProgressLine() {
        const firstNode = markersRow.children[0];
        const activeNode = markersRow.children[currentIndex];
        const lastNode = markersRow.children[markersRow.children.length - 1];
        
        const lineBg = document.getElementById("mw-track-line-bg");
        const lineProgress = document.getElementById("mw-track-line-progress");
        
        if (!firstNode || !activeNode || !lastNode || !lineBg || !lineProgress) return;
        
        // Calculate centers of first, active, and last dots relative to mw-track-content
        const markersRowOffset = markersRow.offsetLeft;
        
        const startX = markersRowOffset + firstNode.offsetLeft + (firstNode.clientWidth / 2);
        const activeX = markersRowOffset + activeNode.offsetLeft + (activeNode.clientWidth / 2);
        const endX = markersRowOffset + lastNode.offsetLeft + (lastNode.clientWidth / 2);
        
        lineBg.style.left = `${startX}px`;
        lineBg.style.width = `${endX - startX}px`;
        
        lineProgress.style.left = `${startX}px`;
        lineProgress.style.width = `${activeX - startX}px`;
      }
      
      function selectMilestone(index) {
        if (filteredEvents.length === 0) return;
        
        if (index < 0) index = 0;
        if (index >= filteredEvents.length) index = filteredEvents.length - 1;
        
        const oldActive = markersRow.querySelector(".mw-marker-node.active");
        if (oldActive) oldActive.classList.remove("active");
        
        currentIndex = index;
        
        const newActive = markersRow.children[currentIndex];
        if (newActive) {
          newActive.classList.add("active");
          
          // Center the active dot in the scroll view using robust client coordinates
          const scrollAreaWidth = trackScrollArea.clientWidth;
          const markerLeft = newActive.getBoundingClientRect().left - markersRow.getBoundingClientRect().left;
          const markerWidth = newActive.clientWidth;
          
          trackScrollArea.scrollTo({
            left: markerLeft - (scrollAreaWidth / 2) + (markerWidth / 2),
            behavior: "smooth"
          });
        }
        
        updateProgressLine();
        updateCardContent();
        
        btnPrev.disabled = currentIndex === 0;
        btnNext.disabled = currentIndex === filteredEvents.length - 1;
        
        currentIndexEl.textContent = currentIndex + 1;
        totalCountEl.textContent = filteredEvents.length;

        // Synchronize with TimelineJS if available
        const event = filteredEvents[currentIndex];
        if (event && window.timeline && typeof window.timeline.goTo === 'function') {
          if (event.originalIndex !== undefined) {
            window.timeline.goTo(event.originalIndex + 1);
          }
        }
      }
      
      function updateCardContent() {
        const event = filteredEvents[currentIndex];
        if (!event) {
          cardTitle.textContent = "No milestones selected";
          cardDesc.innerHTML = "";
          cardDate.textContent = "";
          cardCategory.textContent = "";
          mediaContainer.innerHTML = "";
          return;
        }
        
        const headline = event.text.headline;
        const text = event.text.text;
        const year = event.start_date.year;
        const monthNames = ["", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
        const monthStr = event.start_date.month ? monthNames[parseInt(event.start_date.month)] + " " : "";
        const dateText = `${monthStr}${year}`;
        
        const cardInner = document.getElementById("mw-card");
        cardInner.style.opacity = 0.4;
        
        setTimeout(() => {
          cardDate.textContent = dateText;
          cardCategory.textContent = event.categoryName || "AI Milestone";
          cardTitle.innerHTML = headline;
          cardDesc.innerHTML = text;
          
          renderMedia(event.media);
          
          cardInner.style.opacity = 1;
        }, 150);
      }
      
      function renderMedia(media) {
        mediaContainer.innerHTML = "";
        if (!media || !media.url) {
          mediaContainer.innerHTML = `<div class="mw-maps-card"><span class="mw-maps-icon">🧠</span><div class="mw-maps-caption">AI Milestones</div></div>`;
          return;
        }
        
        const url = media.url;
        const caption = media.caption || "";
        const headline = filteredEvents[currentIndex].text.headline;
        
        if (url.includes("youtube.com") || url.includes("youtu.be")) {
          let videoId = "";
          if (url.includes("v=")) {
            const parts = url.split("v=");
            if (parts[1]) videoId = parts[1].split("&")[0];
          } else if (url.includes("embed/")) {
            const parts = url.split("embed/");
            if (parts[1]) videoId = parts[1].split("?")[0];
          } else if (url.includes("youtu.be/")) {
            const parts = url.split("youtu.be/");
            if (parts[1]) videoId = parts[1].split("?")[0];
          }
          
          mediaContainer.innerHTML = `
            <div style="width: 100%; height: 100%; display: flex; flex-direction: column;">
              <iframe class="mw-iframe" src="https://www.youtube.com/embed/${videoId}" allowfullscreen></iframe>
              <div class="mw-media-caption-bar" style="font-size: 0.75rem; text-align: center; padding: 8px; color: #94a3b8; background: #0b0f19;">
                ${caption}
              </div>
            </div>
          `;
        }
        else if (url.includes("maps.app.goo.gl") || url.includes("google.com/maps")) {
          mediaContainer.innerHTML = `
            <div class="mw-maps-card">
              <span class="mw-maps-icon">📍</span>
              <h5 style="margin: 0 0 5px 0; font-weight: bold; color: inherit; font-size: 1.1rem;">Location Landmark</h5>
              <div class="mw-maps-caption">${caption}</div>
              <a href="${url}" target="_blank" class="mw-maps-btn">Open in Google Maps ↗</a>
            </div>
          `;
        }
        else {
          mediaContainer.innerHTML = `
            <div style="position: relative; width: 100%; height: 100%; overflow: hidden; display: flex; flex-direction: column;">
              <img class="mw-img" src="${url}" alt="${headline}" onerror="this.style.display='none'; this.nextElementSibling.style.display='flex';">
              <div class="mw-media-fallback" style="display: none; width: 100%; height: 100%; min-height: 250px; background: linear-gradient(135deg, #1e1b4b, #2e1065); align-items: center; justify-content: center; flex-direction: column; color: #a5b4fc; padding: 20px; text-align: center;">
                <span style="font-size: 3rem; margin-bottom: 10px;">🧠</span>
                <span style="font-weight: 800; font-size: 1.1rem; margin-bottom: 5px;">${headline}</span>
                <span style="font-size: 0.8rem; opacity: 0.8;">${caption}</span>
              </div>
              <div class="mw-media-caption-bar" style="font-size: 0.75rem; text-align: center; padding: 8px; color: #94a3b8; background: #0b0f19; width: 100%;">
                ${caption}
              </div>
            </div>
          `;
        }

        // Apply theme colors dynamically to elements created in JS
        setTimeout(applyThemeStylesToDynamicContent, 0);
      }

      function applyThemeStylesToDynamicContent() {
        const isLight = document.body.classList.contains("light-theme");
        const captionBars = mediaContainer.querySelectorAll(".mw-media-caption-bar");
        captionBars.forEach(bar => {
          if (isLight) {
            bar.style.background = "#e2e8f0";
            bar.style.color = "#475569";
          } else {
            bar.style.background = "#0b0f19";
            bar.style.color = "#94a3b8";
          }
        });
      }

      // Hook into theme switch event if it exists, or check body changes
      const observer = new MutationObserver(applyThemeStylesToDynamicContent);
      observer.observe(document.body, { attributes: true, attributeFilter: ["class"] });
      
      function toggleAutoplay() {
        if (autoplayInterval) {
          pauseAutoplay();
        } else {
          startAutoplay();
        }
      }
      
      function startAutoplay() {
        if (autoplayInterval) clearInterval(autoplayInterval);
        
        playIcon.style.display = "none";
        pauseIcon.style.display = "inline";
        btnPlay.classList.add("active");
        
        const speed = parseInt(autoplaySpeedSelect.value) || 5000;
        
        autoplayInterval = setInterval(() => {
          if (currentIndex < filteredEvents.length - 1) {
            selectMilestone(currentIndex + 1);
          } else {
            selectMilestone(0);
          }
        }, speed);
      }
      
      function pauseAutoplay() {
        if (autoplayInterval) {
          clearInterval(autoplayInterval);
          autoplayInterval = null;
        }
        playIcon.style.display = "inline";
        pauseIcon.style.display = "none";
        btnPlay.classList.remove("active");
      }
      
      function filterMilestones() {
        const activePill = filterPills.querySelector(".mw-pill.active");
        const category = activePill ? activePill.dataset.category : "all";
        const searchQuery = searchInput.value.toLowerCase().trim();
        
        filteredEvents = events.filter(event => {
          const matchesCategory = (category === "all" || event.category === category);
          
          const titleMatch = event.text.headline.toLowerCase().includes(searchQuery);
          const descMatch = event.text.text.toLowerCase().includes(searchQuery);
          const yearMatch = event.start_date.year.includes(searchQuery);
          const matchesSearch = !searchQuery || titleMatch || descMatch || yearMatch;
          
          return matchesCategory && matchesSearch;
        });
        
        currentIndex = 0;
        renderMarkers();
        selectMilestone(0);
      }
      
      btnPrev.addEventListener("click", () => {
        selectMilestone(currentIndex - 1);
        pauseAutoplay();
      });
      
      btnNext.addEventListener("click", () => {
        selectMilestone(currentIndex + 1);
        pauseAutoplay();
      });
      
      btnPlay.addEventListener("click", toggleAutoplay);
      
      autoplaySpeedSelect.addEventListener("change", () => {
        if (autoplayInterval) {
          startAutoplay();
        }
      });
      
      searchInput.addEventListener("input", () => {
        filterMilestones();
        pauseAutoplay();
      });
      
      filterPills.addEventListener("click", (e) => {
        const pill = e.target.closest(".mw-pill");
        if (!pill) return;
        
        filterPills.querySelectorAll(".mw-pill").forEach(p => p.classList.remove("active"));
        pill.classList.add("active");
        
        filterMilestones();
        pauseAutoplay();
      });
      
      document.getElementById("mw-scroll-left").addEventListener("click", () => {
        trackScrollArea.scrollBy({ left: -200, behavior: "smooth" });
      });
      
      document.getElementById("mw-scroll-right").addEventListener("click", () => {
        trackScrollArea.scrollBy({ left: 200, behavior: "smooth" });
      });
      
      const container = document.getElementById("milestone-walker");
      document.addEventListener("keydown", (e) => {
        const rect = container.getBoundingClientRect();
        // Check if any part of the container is visible in the viewport
        const inView = (rect.top < (window.innerHeight || document.documentElement.clientHeight) && rect.bottom > 0);
        
        if (inView) {
          if (e.key === "ArrowLeft") {
            selectMilestone(currentIndex - 1);
            pauseAutoplay();
            e.preventDefault();
          } else if (e.key === "ArrowRight") {
            selectMilestone(currentIndex + 1);
            pauseAutoplay();
            e.preventDefault();
          }
        }
      });

      // Mobile Touch Swiping Support
      let touchStartX = 0;
      let touchStartY = 0;
      const card = document.getElementById("mw-card");
      
      card.addEventListener("touchstart", (e) => {
        touchStartX = e.changedTouches[0].screenX;
        touchStartY = e.changedTouches[0].screenY;
      }, { passive: true });
      
      card.addEventListener("touchend", (e) => {
        const touchEndX = e.changedTouches[0].screenX;
        const touchEndY = e.changedTouches[0].screenY;
        handleSwipe(touchStartX, touchStartY, touchEndX, touchEndY);
      }, { passive: true });
      
      function handleSwipe(startX, startY, endX, endY) {
        const diffX = endX - startX;
        const diffY = endY - startY;
        
        // Trigger swipe navigation if horizontal movement is dominant and > 50px
        if (Math.abs(diffX) > Math.abs(diffY) && Math.abs(diffX) > 50) {
          if (diffX > 0) {
            // Swipe right -> Go to Previous
            if (currentIndex > 0) {
              selectMilestone(currentIndex - 1);
              pauseAutoplay();
            }
          } else {
            // Swipe left -> Go to Next
            if (currentIndex < filteredEvents.length - 1) {
              selectMilestone(currentIndex + 1);
              pauseAutoplay();
            }
          }
        }
      }
      
      renderMarkers();
      selectMilestone(0);
    }
  });
</script>

---

## 📈 Summary Timeline of AI Milestones & Model Releases

Here is a chronological list of major AI milestones, company foundings, and model releases discussed in this post:

*   **1956:** Dartmouth Workshop (Term 'Artificial Intelligence' Coined)
*   **1966:** ELIZA Chatbot (MIT)
*   **1997:** IBM Deep Blue (Defeats Garry Kasparov)
*   **2010:** DeepMind Founded (London)
*   **2011:** Apple Siri & Google Brain Founded
*   **2012:** ImageNet GPU Deep Learning Breakthrough (AlexNet)
*   **2014:** Google Acquires DeepMind
*   **2015:** OpenAI Founded (San Francisco)
*   **2016:** AlphaGo Defeats Lee Sedol (Seoul)
*   **2017:** AlphaGo Defeats Ke Jie & Transformer Architecture Paper
*   **2018:** OpenAI GPT-1
*   **2019:** OpenAI GPT-2
*   **2020:** OpenAI Scaling Laws Paper, GPT-3, & DeepMind AlphaFold 2 (Protein Folding Solved)
*   **2021:** GitHub Copilot (OpenAI Codex)
*   **2022:** OpenAI ChatGPT (GPT-3.5)
*   **2023:** OpenAI GPT-4, Anthropic Claude 1, & Google Gemini 1.0
*   **2024:** Google Gemini 1.5 Pro, OpenAI GPT-4o, Anthropic Claude 3.5 Sonnet, OpenAI o1, & DeepSeek-V3
*   **2025:** DeepSeek-R1, Anthropic Claude 3.7 Sonnet (Claude Code), & OpenAI GPT-5 (Scaling Plateau)
*   **2026:** Anthropic Claude Sonnet 5, OpenAI GPT-5.5, & Google Gemini 3.1 Pro
