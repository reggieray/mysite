Title: "From a Pun to a 5,000-Year Archive: Showcasing All 60 Games in Game Archive"
Published: 10/27/2026
Lead: "What started as a pun and an urge to play two games in one app turned into an addiction to adding games. Here is a showcase of all 60 playable games in Game Archive—spanning ancient Mesopotamia to early computer labs."
Tags:
- games
- history
- board games
- bored games
- game archive
- flutter
- mobile
- showcase
- indie hacking
- app development
---

<style>
  html {
    scroll-behavior: smooth;
  }
  h2[id], h3[id] {
    scroll-margin-top: 4.5em;
  }
  .archive-hero {
    width: 100%;
    border-radius: 12px;
    box-shadow: 0 8px 30px rgba(0, 0, 0, 0.4);
    margin: 1.5rem 0 2rem 0;
    display: block;
  }
  .era-nav {
    display: flex;
    flex-wrap: wrap;
    gap: 8px;
    margin: 1.5rem 0 2rem 0;
    padding: 12px;
    background: rgba(255, 255, 255, 0.04);
    border-radius: 10px;
    border: 1px solid rgba(255, 255, 255, 0.08);
  }
  .era-nav-link {
    font-size: 0.85rem;
    font-weight: 600;
    padding: 6px 14px;
    border-radius: 20px;
    background: rgba(235, 134, 42, 0.12);
    color: #eb862a !important;
    border: 1px solid rgba(235, 134, 42, 0.3);
    text-decoration: none !important;
    transition: all 0.2s ease;
  }
  .era-nav-link:hover {
    background: #eb862a;
    color: #fff !important;
  }
  .game-archive-grid {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
    gap: 22px;
    margin: 1.5rem 0 3rem 0;
  }
  .game-card {
    background: rgba(255, 255, 255, 0.04);
    border: 1px solid rgba(255, 255, 255, 0.1);
    border-radius: 12px;
    overflow: hidden;
    display: flex;
    flex-direction: column;
    transition: transform 0.2s ease, box-shadow 0.2s ease, border-color 0.2s ease;
  }
  .game-card:hover {
    transform: translateY(-4px);
    box-shadow: 0 10px 28px rgba(0, 0, 0, 0.35);
    border-color: rgba(235, 134, 42, 0.4);
  }
  .game-card-img-wrapper {
    width: 100%;
    background: #000;
    border-bottom: 1px solid rgba(255, 255, 255, 0.08);
  }
  .game-card-img-wrapper img {
    width: 100%;
    height: auto;
    display: block;
  }
  .game-card-body {
    padding: 16px;
    display: flex;
    flex-direction: column;
    flex-grow: 1;
  }
  .game-card-title {
    font-size: 1.15rem;
    font-weight: 700;
    margin: 0 0 4px 0;
    color: #ffffff;
  }
  .game-card-subtitle {
    font-size: 0.8rem;
    font-style: italic;
    color: #a0aec0;
    margin-bottom: 10px;
  }
  .game-card-badge {
    display: inline-block;
    font-size: 0.72rem;
    font-weight: 600;
    text-transform: uppercase;
    letter-spacing: 0.5px;
    padding: 3px 8px;
    border-radius: 4px;
    background: rgba(235, 134, 42, 0.15);
    color: #eb862a;
    border: 1px solid rgba(235, 134, 42, 0.3);
    margin-bottom: 10px;
    align-self: flex-start;
  }
  .game-card-desc {
    font-size: 0.88rem;
    line-height: 1.55;
    color: #cbd5e1;
    margin: 0;
  }
  .era-intro {
    background: rgba(235, 134, 42, 0.08);
    border-left: 4px solid #eb862a;
    padding: 14px 18px;
    margin: 1.5rem 0 1.5rem 0;
    border-radius: 0 8px 8px 0;
    font-size: 0.95rem;
    line-height: 1.6;
    color: #e2e8f0;
  }
  .cta-banner {
    background: linear-gradient(135deg, rgba(235, 134, 42, 0.18) 0%, rgba(99, 102, 241, 0.18) 100%);
    border: 1px solid rgba(235, 134, 42, 0.35);
    border-radius: 12px;
    padding: 28px 24px;
    margin: 2.5rem 0;
    text-align: center;
  }
  .cta-button {
    display: inline-block;
    background: #eb862a;
    color: #ffffff !important;
    font-weight: 700;
    font-size: 1rem;
    padding: 12px 28px;
    border-radius: 30px;
    text-decoration: none !important;
    margin-top: 14px;
    box-shadow: 0 4px 16px rgba(235, 134, 42, 0.45);
    transition: transform 0.2s, background 0.2s;
  }
  .cta-button:hover {
    background: #f09540;
    transform: translateY(-2px);
  }
  body.light-theme .game-card {
    background: #ffffff;
    border-color: rgba(0, 0, 0, 0.1);
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.05);
  }
  body.light-theme .game-card-title {
    color: #1e293b;
  }
  body.light-theme .game-card-subtitle {
    color: #64748b;
  }
  body.light-theme .game-card-desc {
    color: #334155;
  }
  body.light-theme .era-intro {
    background: rgba(235, 134, 42, 0.08);
    color: #1e293b;
  }
  body.light-theme .era-nav {
    background: rgba(0, 0, 0, 0.03);
    border-color: rgba(0, 0, 0, 0.08);
  }
</style>

# From a Pun to a 5,000-Year Archive: Showcasing All 60 Games

<img class="archive-hero" src="/posts/images/game-archive/feature-graphic.png" alt="Game Archive Feature Graphic">

A couple of months ago, I published a post titled **[Coding a 4,500-Year-Old Game: Why I Dropped .NET MAUI for Flutter to Build Bored Games](/posts/2026/coding-a-4500-year-old-game-dropping-maui-for-flutter)**. 

In that post, I talked about how the project originally started as a pun: **Bored Games** (a play on "board games"). My initial goal was really simple: I watched Irving Finkel at the British Museum play the Royal Game of Ur against Tom Scott on YouTube, thought it was cool that a game from 2600 BCE was still playable, and wanted an app where I could play both Chess and the Royal Game of Ur without having to download a bunch of separate apps loaded with ads.

Since I couldn't find one, I started building it myself.

At the time, the plan was just to add Chess, Ur, maybe Checkers and Mancala, and leave it at that.

Instead, I got addicted to adding new games.

---

## 🎲 Finding More Games to Add

Once the core app was running smoothly in Flutter, I started looking around to see what other games I could add. That's when I really got into it. Every time I finished adding one game, I found myself searching for the next one.

To be honest, I hardly knew about most of the games I ended up adding. Aside from Chess, Checkers, and what I had seen in that Royal Game of Ur video, most of them were completely new to me. But through the process of looking for games and implementing them, I got to learn about how each one worked, dig into its history, and play a few good games along the way.

It ended up combining two interests of mine: history and games.

What started out as a small app with just a couple of games eventually turned into 60. As the collection grew, referring to it as **Game Archive** felt more fitting than just a simple minigame app.

---

## 🕹️ Not My Usual Coding Post

Most of the articles on this blog are about coding, .NET, microservice architectures, or technical migrations. 

This post isn't like that. I'm skipping the code snippets and architectural breakdowns this time around. Instead, I just wanted to showcase the app and all 60 games I've added so far, along with a little background on each one.

If you have some time and want to give any of them a try, feel free to check out the app. It's completely free, works offline, and supports local pass-and-play as well as AI opponents.

<div class="cta-banner">
  <h3 style="margin: 0 0 8px 0; color: #fff;">Try Game Archive on Android</h3>
  <p style="margin: 0; color: #cbd5e1; font-size: 0.95rem;">100% offline • No accounts • No Wi-Fi required • Smart AI Personas • Local Pass & Play</p>
  <a class="cta-button" href="https://play.google.com/store/apps/details?id=dev.matthewregis.bored_games" target="_blank" rel="noopener">📥 Download Free on Google Play</a>
</div>

---

## 🏛️ Quick Era Navigation

Jump to any era to browse the games:

<div class="era-nav">
  <a class="era-nav-link" href="#bronze-age">🏺 Bronze Age (~3500 – 1000 BCE)</a>
  <a class="era-nav-link" href="#classical-antiquity">🏛️ Classical Antiquity (1000 BCE – 500 CE)</a>
  <a class="era-nav-link" href="#medieval-silk-road">⚔️ Medieval &amp; Silk Road (500 – 1500 CE)</a>
  <a class="era-nav-link" href="#early-modern">⛵ Early Modern (1500 – 1900 CE)</a>
  <a class="era-nav-link" href="#modern-era">💡 20th Century &amp; Beyond (1900 – Present)</a>
</div>

---

<h2 id="bronze-age">🏺 Bronze Age (~3500 – 1000 BCE)</h2>

<div class="era-intro">
  Games dating back to ancient Mesopotamia, Egypt, and the Levant, including some of the earliest known dice, race, and counting games.
</div>

<div class="game-archive-grid">

  <!-- 1. MANCALA -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/mancala.png" alt="Mancala Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Mancala (Kalah)</h3>
      <div class="game-card-subtitle">Seed Sowing & Pit Clears</div>
      <span class="game-card-badge">c. 6000 BCE • Ancient Africa & Levant</span>
      <p class="game-card-desc">One of humanity's oldest mathematical counting games. Players distribute seeds around two rows of carved wooden pits, calculating sowings to score bonus turns and capture opponent storehouses.</p>
    </div>
  </div>

  <!-- 2. SENET -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/senet.png" alt="Senet Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Senet</h3>
      <div class="game-card-subtitle">30 Underworld Squares of the Pharaohs</div>
      <span class="game-card-badge">c. 3100 BCE • Ancient Egypt</span>
      <p class="game-card-desc">Buried in the tomb of King Tutankhamun to guide royals through the afterlife. Pawns navigate an S-shaped underworld grid guided by four casting sticks past treacherous hazard squares.</p>
    </div>
  </div>

  <!-- 3. MEHEN -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/mehen.png" alt="Mehen Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Mehen</h3>
      <div class="game-card-subtitle">The Coiled Serpent Game</div>
      <span class="game-card-badge">c. 3000 BCE • Ancient Egypt</span>
      <p class="game-card-desc">Played on a coiled serpent board honoring the protector of Ra. Players race spherical marbles into the serpent's head, unleashing fierce lion guardian pieces to hunt down opposing tokens.</p>
    </div>
  </div>

  <!-- 4. BACKGAMMON -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/backgammon.png" alt="Backgammon Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Backgammon</h3>
      <div class="game-card-subtitle">Royal Tables of Persia & Rome</div>
      <span class="game-card-badge">c. 3000 BCE • Mesopotamia & Persia</span>
      <p class="game-card-desc">The ultimate race of probability and positioning across 24 points. Balance aggressive blots, tactical prime walls, and hit-to-bar escapes in one of the most enduring games ever created.</p>
    </div>
  </div>

  <!-- 5. GAME OF UR -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/game_of_ur.png" alt="Royal Game of Ur Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Royal Game of Ur</h3>
      <div class="game-card-subtitle">The 4,600-Year-Old Royal Race</div>
      <span class="game-card-badge">c. 2600 BCE • Ancient Sumer</span>
      <p class="game-card-desc">The catalyst that launched this app! Reconstructed by Dr. Irving Finkel from Babylonian cuneiform tablets, players roll four tetrahedral dice across a rosette-studded bridge.</p>
    </div>
  </div>

  <!-- 6. HOUNDS & JACKALS -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/hounds_and_jackals.png" alt="Hounds and Jackals Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Hounds & Jackals</h3>
      <div class="game-card-subtitle">The Game of 58 Holes</div>
      <span class="game-card-badge">c. 2000 BCE • Ancient Egypt & Thebes</span>
      <p class="game-card-desc">Found in royal Theban tombs carved with ebony and ivory dog heads. Carved pegs navigate paired tracks with secret interconnected conduits racing toward the goal palm tree.</p>
    </div>
  </div>

  <!-- 7. NINE MEN'S MORRIS -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/nine_mens_morris.png" alt="Nine Men's Morris Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Nine Men's Morris</h3>
      <div class="game-card-subtitle">The Mill: Ancient Square Grids</div>
      <span class="game-card-badge">c. 1400 BCE • Egypt & Rome</span>
      <p class="game-card-desc">Scratched into stone steps across Europe and Egyptian temples. Players drop and slide nine counters along concentric squares, forming "mills" of three to capture opposing pieces.</p>
    </div>
  </div>

  <!-- 8. TIC-TAC-TOE -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/tic_tac_toe.png" alt="Tic-Tac-Toe Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Tic-Tac-Toe (Terni Lapilli)</h3>
      <div class="game-card-subtitle">Roman Forum Pebble Duel</div>
      <span class="game-card-badge">c. 1300 BCE • Egypt & Rome</span>
      <p class="game-card-desc">The timeless three-in-a-row alignment classic. Play either traditional free grid placement or the original Roman variation where players slide three reusable pebbles across the grid.</p>
    </div>
  </div>

</div>

---

<h2 id="classical-antiquity">🏛️ Classical Antiquity (1000 BCE – 500 CE)</h2>

<div class="era-intro">
  Games played across ancient China, Greece, Rome, Mesoamerica, and Scandinavia.
</div>

<div class="game-archive-grid">

  <!-- 9. GO -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/go_weiqi.png" alt="Go Weiqi Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Go (Weiqi)</h3>
      <div class="game-card-subtitle">The Scholar's Art of Encirclement</div>
      <span class="game-card-badge">c. 548 BCE • Ancient China (Zhou Dynasty)</span>
      <p class="game-card-desc">One of the four traditional scholar arts of imperial China. Playable on 9x9, 13x13, and full 19x19 grids. Encircle empty territory with black and white stones in a game of virtually infinite depth.</p>
    </div>
  </div>

  <!-- 10. PETTEIA -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/petteia.png" alt="Petteia Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Petteia</h3>
      <div class="game-card-subtitle">Homeric Strategy of the Greeks</div>
      <span class="game-card-badge">c. 500 BCE • Classical Greece (Athens)</span>
      <p class="game-card-desc">Referenced by Homer in the <em>Odyssey</em> and painted on ancient amphorae with Achilles and Ajax. Two armies of stones maneuver across an 8x8 grid utilizing custodial flanking captures.</p>
    </div>
  </div>

  <!-- 11. XIANGQI -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/xiangqi.png" alt="Xiangqi Chinese Chess Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Xiangqi (Chinese Chess)</h3>
      <div class="game-card-subtitle">Cannons & The Celestial Chu River</div>
      <span class="game-card-badge">c. 200 BCE • Han Dynasty China</span>
      <p class="game-card-desc">A dramatic simulation of imperial warfare. Features leaping Cannons that need screens to strike, Chariots charging along lines, and Generals confined to their palace fortress.</p>
    </div>
  </div>

  <!-- 12. SNAKES & LADDERS -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/snakes_and_ladders.png" alt="Snakes and Ladders Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Snakes & Ladders</h3>
      <div class="game-card-subtitle">Moksha Patam: The Karmic Path</div>
      <span class="game-card-badge">c. 200 BCE • Ancient India</span>
      <p class="game-card-desc">Originally created as a spiritual meditation on karma. Ladders represented virtuous deeds carrying the soul upward, while serpents represented vices dragging players back downward.</p>
    </div>
  </div>

  <!-- 13. PATOLLI -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/patolli.png" alt="Patolli Aztec Cross Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Patolli</h3>
      <div class="game-card-subtitle">The Aztec Sacred Calendar Cross</div>
      <span class="game-card-badge">c. 200 BCE • Mesoamerica & Aztecs</span>
      <p class="game-card-desc">Played on a woven cross mat of 52 squares matching the Mesoamerican solar cycle. Cast drilled black beans, navigate the cross arms, and stake wagers under the god Macuilxochitl.</p>
    </div>
  </div>

  <!-- 14. LUDUS LATRUNCULORUM -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/ludus_latrunculorum.png" alt="Ludus Latrunculorum Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Ludus Latrunculorum</h3>
      <div class="game-card-subtitle">The Roman Game of Mercenaries</div>
      <span class="game-card-badge">c. 150 BCE • Roman Republic & Empire</span>
      <p class="game-card-desc">The favorite tactical board game of Roman legionaries across garrison camps. Form coordinated battle lines and surround opposing glass counters to immobilize enemy troops.</p>
    </div>
  </div>

  <!-- 15. XII SCRIPTA -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/xii_scripta.png" alt="XII Scripta Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">XII Scripta</h3>
      <div class="game-card-subtitle">Twelve Markings of Imperial Rome</div>
      <span class="game-card-badge">c. 100 BCE • Imperial Rome</span>
      <p class="game-card-desc">Carved into tavern tables and bathhouses across Pompeii and Rome. Three parallel rows of twelve letters spell witty epigrams as players roll three dice to race tokens off the board.</p>
    </div>
  </div>

  <!-- 16. YUT NORI -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/yut_nori.png" alt="Yut Nori Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Yut Nori</h3>
      <div class="game-card-subtitle">Three Kingdoms Korean Lunar Race</div>
      <span class="game-card-badge">c. 57 BCE • Three Kingdoms Korea</span>
      <p class="game-card-desc">A lively Korean traditional holiday race. Cast four wooden sticks (<em>yut</em>) to advance tokens around a four-cornered circuit, utilizing diagonal shortcut intersections.</p>
    </div>
  </div>

  <!-- 17. HNEFATAFL -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/hnefatafl.png" alt="Hnefatafl Viking Chess Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Hnefatafl</h3>
      <div class="game-card-subtitle">Viking Chess & The King's Siege</div>
      <span class="game-card-badge">c. 400 CE • Norse Scandinavia</span>
      <p class="game-card-desc">The legendary asymmetric Norse epic. A central King and his brave guard are encircled by 24 attacking raiders. The King must break through the encirclement to reach corner sanctuaries.</p>
    </div>
  </div>

</div>

---

<h2 id="medieval-silk-road">⚔️ Medieval &amp; Silk Road (500 – 1500 CE)</h2>

<div class="era-intro">
  Games from the medieval period, including early ancestors of chess from India, variants across Asia, and Viking siege games.
</div>

<div class="game-archive-grid">

  <!-- 18. CHATURANGA -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/chaturanga.png" alt="Chaturanga Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Chaturanga</h3>
      <div class="game-card-subtitle">The Ancestral Indian Four-Branch Army</div>
      <span class="game-card-badge">c. 550 CE • Gupta Empire India</span>
      <p class="game-card-desc">The grand matriarch of all chess variants. Command the four military branches of ancient India: Foot Soldiers, War Horses, Elephants, and Chariots alongside the Raja.</p>
    </div>
  </div>

  <!-- 19. PACHISI -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/pachisi.png" alt="Pachisi Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Pachisi (Chaupar)</h3>
      <div class="game-card-subtitle">The Royal Cross Game of India</div>
      <span class="game-card-badge">c. 600 CE • Ancient India</span>
      <p class="game-card-desc">The national game of India and progenitor of Ludo. Cast cowrie shells to move tokens around an embroidered cloth cross, capturing opponents and escorting four pawns to the central Charkoni.</p>
    </div>
  </div>

  <!-- 20. PULUC -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/puluc.png" alt="Puluc Maya Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Puluc (Boolik)</h3>
      <div class="game-card-subtitle">The Maya Ritual Running-Fight Game</div>
      <span class="game-card-badge">c. 600 CE • Classic Maya Civilization</span>
      <p class="game-card-desc">Played by Highland Maya warriors using split corn kernels. March across linear battlefield rungs, capturing enemy warriors by landing on top of them and hauling captives home.</p>
    </div>
  </div>

  <!-- 21. GOMOKU -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/gomoku.png" alt="Gomoku Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Gomoku</h3>
      <div class="game-card-subtitle">Five Stones in a Row</div>
      <span class="game-card-badge">c. 700 CE • Nara Period Japan</span>
      <p class="game-card-desc">Pure tactical alignment on a Go grid. Players alternate placing black and white stones, maneuvering to create an unbroken row of five while urgently defending against opponent threats.</p>
    </div>
  </div>

  <!-- 22. BRANDUBH -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/brandubh.png" alt="Brandubh Celtic Chess Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Brandubh</h3>
      <div class="game-card-subtitle">Black Raven: Celtic Tafl Duel</div>
      <span class="game-card-badge">c. 900 CE • Medieval Celtic Ireland</span>
      <p class="game-card-desc">Celebrated in ancient Gaelic bardic poetry. A compact 7x7 board where an Irish High King and four loyal defenders fight to escape eight encroaching invaders.</p>
    </div>
  </div>

  <!-- 23. JANGGI -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/janggi.png" alt="Janggi Korean Chess Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Janggi</h3>
      <div class="game-card-subtitle">Korean Chess & Fortress Duels</div>
      <span class="game-card-badge">c. 950 CE • Goryeo Dynasty Korea</span>
      <p class="game-card-desc">Korean chess featuring expansive royal palaces, versatile jumping cannons that fly across fortress diagonals, and pre-match piece transposition choices.</p>
    </div>
  </div>

  <!-- 24. BAGH-CHAL -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/bagh_chal.png" alt="Bagh-Chal Tigers and Goats Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Bagh-Chal</h3>
      <div class="game-card-subtitle">Tigers & Goats of Nepal</div>
      <span class="game-card-badge">c. 1000 CE • Himalayan Nepal</span>
      <p class="game-card-desc">An asymmetric Himalayan predator-prey duel. One player commands 4 prowling tigers seeking leaping captures, while the other coordinates 20 goats to pen the predators in.</p>
    </div>
  </div>

  <!-- 25. SHOGI -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/shogi.png" alt="Shogi Japanese Chess Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Shogi</h3>
      <div class="game-card-subtitle">The Generals' Chess & Drop Rule</div>
      <span class="game-card-badge">c. 1058 CE • Heian Period Japan</span>
      <p class="game-card-desc">Japanese chess featuring its revolutionary Drop Rule: captured enemy pieces become mercenaries you can deploy anywhere on the board, leading to explosive tactical attacks.</p>
    </div>
  </div>

  <!-- 26. CHECKERS -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/checkers.png" alt="Checkers Draughts Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Checkers (Draughts)</h3>
      <div class="game-card-subtitle">Diagonal Leaps & Crowned Kings</div>
      <span class="game-card-badge">c. 1100 CE • Medieval France</span>
      <p class="game-card-desc">Born from transplanting Alquerque leaps onto a 64-square checkered board. Easy to learn, yet hiding deep combinations of forced multi-jump chains and king promotions.</p>
    </div>
  </div>

  <!-- 27. DOMINOES -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/dominoes.png" alt="Dominoes Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Dominoes</h3>
      <div class="game-card-subtitle">Bone Tiles: Block & Draw</div>
      <span class="game-card-badge">c. 1120 CE • Song Dynasty China</span>
      <p class="game-card-desc">Created from pairs of rolled dice engraved on bone and ivory tiles. Match pips at open chain ends in Block and Draw modes, keeping your hand hidden and counting dots.</p>
    </div>
  </div>

  <!-- 28. ALQUERQUE -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/alquerque.png" alt="Alquerque Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Alquerque (Qirkat)</h3>
      <div class="game-card-subtitle">Alfonso X's Book of Games</div>
      <span class="game-card-badge">1283 CE • Moorish Spain & Castile</span>
      <p class="game-card-desc">Preserved in King Alfonso X's magnificent 13th-century illuminated manuscript. The true tactical father of modern Draughts, with 12 pieces per side leaping along grid lines.</p>
    </div>
  </div>

  <!-- 29. FOX & GEESE -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/fox_and_geese.png" alt="Fox and Geese Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Fox & Geese</h3>
      <div class="game-card-subtitle">The Medieval Tafl Hunt</div>
      <span class="game-card-badge">c. 1300 CE • Medieval Europe</span>
      <p class="game-card-desc">An asymmetric medieval hunt on a cross-shaped board. A lone hungry Fox tries to jump and capture unwary birds, while a flock of 13 Geese aims to box the Fox into a corner.</p>
    </div>
  </div>

  <!-- 30. MAKRUK -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/makruk.png" alt="Makruk Thai Chess Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Makruk</h3>
      <div class="game-card-subtitle">Siamese Chess of Thailand</div>
      <span class="game-card-badge">c. 1350 CE • Siam (Thailand)</span>
      <p class="game-card-desc">The closest living descendant to ancient Indian Chaturanga. Pawns start on the 3rd rank, leading to thoughtful positional battles with Seeds, Nobles, and War Elephants.</p>
    </div>
  </div>

  <!-- 31. CHESS -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/chess.png" alt="Chess Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Chess</h3>
      <div class="game-card-subtitle">The Definitive Game of Kings</div>
      <span class="game-card-badge">c. 1475 CE • Renaissance Europe</span>
      <p class="game-card-desc">The classic game of kings and grandmasters. Features Stockfish 16 engine analysis, opening book moves, tactical checkmate puzzles, and full offline play.</p>
    </div>
  </div>

</div>

---

<h2 id="early-modern">⛵ Early Modern (1500 – 1900 CE)</h2>

<div class="era-intro">
  Games from the 16th to 19th centuries across Europe, Africa, the Pacific, and Asia.
</div>

<div class="game-archive-grid">

  <!-- 32. LIAR'S DICE -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/liars_dice.png" alt="Liar's Dice Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Liar's Dice</h3>
      <div class="game-card-subtitle">Incan Dudo & Pirate Bluffing</div>
      <span class="game-card-badge">c. 1500 CE • Incan Empire & Caribbean</span>
      <p class="game-card-desc">A game of psychology and hidden probabilities. Shake dice under leather cups, raise bids on table-wide face totals, and call your opponent's bluff before losing your dice.</p>
    </div>
  </div>

  <!-- 33. CRIBBAGE -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/cribbage.png" alt="Cribbage Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Cribbage</h3>
      <div class="game-card-subtitle">Sir John Suckling's Pegboard Duel</div>
      <span class="game-card-badge">c. 1630 CE • England</span>
      <p class="game-card-desc">Invented by English poet Sir John Suckling. Combine card combinations for Fifteen-Twos, Runs, and Pairs while leapfrogging scoring pegs along a carved wooden track.</p>
    </div>
  </div>

  <!-- 34. SURAKARTA -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/surakarta.png" alt="Surakarta Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Surakarta</h3>
      <div class="game-card-subtitle">Java's Rollercoaster Corner Loops</div>
      <span class="game-card-badge">c. 1650 CE • Java, Indonesia</span>
      <p class="game-card-desc">Named after the royal city of Surakarta in central Java. Pieces slide on a 6x6 grid, but can only capture enemies by looping completely around circular corner tramway tracks!</p>
    </div>
  </div>

  <!-- 35. FANORONA -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/fanorona.png" alt="Fanorona Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Fanorona</h3>
      <div class="game-card-subtitle">The Royal Battle of Madagascar</div>
      <span class="game-card-badge">c. 1680 CE • Kingdom of Madagascar</span>
      <p class="game-card-desc">The national game of Madagascar, once used to train royal generals. Captures are executed not by landing on pieces, but by either <em>approaching</em> or <em>withdrawing</em> from enemies.</p>
    </div>
  </div>

  <!-- 36. KŌNANE -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/konane.png" alt="Kōnane Hawaiian Checkers Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Kōnane</h3>
      <div class="game-card-subtitle">Hawaiian Royal Lava Slab Leap</div>
      <span class="game-card-badge">c. 1700 CE • Kingdom of Hawaii</span>
      <p class="game-card-desc">Played on black basalt lava slabs with black lava and white coral stones. Kings and chiefs leaped over enemy stones in straight lines until one player had no moves left.</p>
    </div>
  </div>

  <!-- 37. TABLUT -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/tablut.png" alt="Tablut Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Tablut</h3>
      <div class="game-card-subtitle">Sámi Lapland Royal Escape</div>
      <span class="game-card-badge">c. 1732 CE • Sámi Lapland</span>
      <p class="game-card-desc">Documented by botanist Carl Linnaeus during his 1732 expedition to Arctic Lapland. The Swedish King escapes from his central throne through an ambush of 16 raiders.</p>
    </div>
  </div>

  <!-- 38. MORABARABA -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/morabaraba.png" alt="Morabaraba Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Morabaraba</h3>
      <div class="game-card-subtitle">The 12-Cow Cattle Mill of Southern Africa</div>
      <span class="game-card-badge">c. 1750 CE • Southern Africa</span>
      <p class="game-card-desc">Traditionally played on flat stones by herdboys in South Africa and Lesotho. Place and slide 12 "cows" along three concentric squares with diagonal lines to shoot opponent cows.</p>
    </div>
  </div>

  <!-- 39. MŪ TŌRERE -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/mu_torere.png" alt="Mū Tōrere Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Mū Tōrere</h3>
      <div class="game-card-subtitle">The Māori 8-Pointed Star</div>
      <span class="game-card-badge">c. 1780 CE • Māori of New Zealand</span>
      <p class="game-card-desc">A dazzling game of circular geometry played on an 8-pointed star. Pieces can only enter the central vortex if adjacent to an enemy piece, requiring clever traps to blockade opponents.</p>
    </div>
  </div>

  <!-- 40. SEEGA -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/seega.png" alt="Seega Desert Board Game Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Seega</h3>
      <div class="game-card-subtitle">Bedouin Desert Tactics</div>
      <span class="game-card-badge">c. 1800 CE • Egypt & Sinai Bedouin</span>
      <p class="game-card-desc">Played in desert sand dunes using pebbles. Armies are dropped onto a 5x5 board leaving only the center square vacant, followed by furious custodial ambush strikes.</p>
    </div>
  </div>

  <!-- 41. MAHJONG -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/mahjong.png" alt="Mahjong Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Mahjong</h3>
      <div class="game-card-subtitle">Sparrow Tiles: Solitaire, Rummy & Riichi</div>
      <span class="game-card-badge">c. 1850 CE • Qing Dynasty China</span>
      <p class="game-card-desc">The iconic Chinese tile-matching masterwork. Features 144 beautifully rendered bone and bamboo tiles across 3 modes: Solitaire 3D, 2-Player Rummy, and full 4-Player Japanese Riichi.</p>
    </div>
  </div>

  <!-- 42. REVERSI -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/reversi.png" alt="Reversi Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Reversi</h3>
      <div class="game-card-subtitle">The Victorian Othello Disk Flip</div>
      <span class="game-card-badge">1883 CE • Victorian England</span>
      <p class="game-card-desc">"A minute to learn, a lifetime to master." Trap opposing disks between your own to flip entire horizontal, vertical, and diagonal rows in sudden dramatic reversals of fortune.</p>
    </div>
  </div>

  <!-- 43. DOTS & BOXES -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/dots_and_boxes.png" alt="Dots and Boxes Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Dots & Boxes</h3>
      <div class="game-card-subtitle">Édouard Lucas's La Pipopipette</div>
      <span class="game-card-badge">1889 CE • France</span>
      <p class="game-card-desc">Created by the brilliant French mathematician Édouard Lucas. Connect adjacent dots with lines; closing the 4th wall of any 1x1 box claims it and gives you another turn for chain cascades.</p>
    </div>
  </div>

  <!-- 44. CHINESE CHECKERS -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/chinese_checkers.png" alt="Chinese Checkers Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Chinese Checkers</h3>
      <div class="game-card-subtitle">Sternhalma: The German Six-Point Star</div>
      <span class="game-card-badge">1892 CE • Germany</span>
      <p class="game-card-desc">Invented in Germany under the name <em>Sternhalma</em> (star halma). Hop marbles across a 121-intersection hexagram star to be the first to occupy the destination star corner.</p>
    </div>
  </div>

</div>

---

<h2 id="modern-era">💡 20th Century &amp; Beyond (1900 – Present)</h2>

<div class="era-intro">
  A mix of 20th-century strategy games, codebreakers, and early computer games from university and research labs.
</div>

<div class="game-archive-grid">

  <!-- 45. JUNGLE -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/jungle.png" alt="Jungle Dou Shou Qi Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Jungle (Dou Shou Qi)</h3>
      <div class="game-card-subtitle">Chinese Animal Chess & River Leaps</div>
      <span class="game-card-badge">c. 1900 CE • China</span>
      <p class="game-card-desc">Command 8 animal ranks across rivers, traps, and dens. The tiny Rat can swim and defeat the mighty Elephant, while Lions and Tigers leap clean across the central river.</p>
    </div>
  </div>

  <!-- 46. SEA BATTLE -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/sea_battle.png" alt="Sea Battle Battleship Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Sea Battle</h3>
      <div class="game-card-subtitle">10x10 Salvo Fleet Command</div>
      <span class="game-card-badge">1931 CE • France & USA</span>
      <p class="game-card-desc">The legendary naval radar combat game. Secretly position your armada of carriers, battleships, cruisers, subs, and destroyers, firing blind salvos into the fog of war.</p>
    </div>
  </div>

  <!-- 47. HEX -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/hex.png" alt="Hex Board Game Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Hex</h3>
      <div class="game-card-subtitle">Piet Hein & John Nash's Masterwork</div>
      <span class="game-card-badge">1942 CE • Denmark & Princeton</span>
      <p class="game-card-desc">Invented independently by polymath Piet Hein and Nobel laureate John Nash. Race to connect opposite sides of a hexagonal board with an unbroken chain—proven to have zero draws!</p>
    </div>
  </div>

  <!-- 48. WORD GAME -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/word_game.png" alt="Word Game Crossword Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Word Game</h3>
      <div class="game-card-subtitle">15x15 Crossword Anagram Builder</div>
      <span class="game-card-badge">1948 CE • New York</span>
      <p class="game-card-desc">Draw 7 letter tiles and construct intersecting crossword puzzles on a 15x15 grid. Hit Double/Triple Letter and Word multipliers and score 50-point Bingo bonuses using the ENABLE1 dictionary.</p>
    </div>
  </div>

  <!-- 49. NIMROD -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/nimrod.png" alt="Nimrod 1951 Computer Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Nimrod</h3>
      <div class="game-card-subtitle">The 1951 Festival of Britain Computer</div>
      <span class="game-card-badge">1951 CE • London & Manchester</span>
      <p class="game-card-desc">Built by Ferranti as a room-sized vacuum tube machine designed solely to play Nim against exhibition attendees (including Alan Turing!). Remove tokens from heaps using binary XOR math.</p>
    </div>
  </div>

  <!-- 50. TENNIS FOR TWO -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/tennis_for_two.png" alt="Tennis for Two Oscilloscope Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Tennis for Two</h3>
      <div class="game-card-subtitle">The 1958 Brookhaven Oscilloscope Pioneer</div>
      <span class="game-card-badge">1958 CE • Brookhaven National Lab</span>
      <p class="game-card-desc">Created by physicist William Higinbotham on a Donner Model 30 analog computer and a 5-inch oscilloscope screen. Adjust ball trajectory over a simulated net years before Pong existed.</p>
    </div>
  </div>

  <!-- 51. MOUSE IN THE MAZE -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/mouse_in_the_maze.png" alt="Mouse in the Maze TX-0 Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Mouse in the Maze</h3>
      <div class="game-card-subtitle">The 1959 MIT TX-0 Light Pen Simulation</div>
      <span class="game-card-badge">1959 CE • MIT Lincoln Lab</span>
      <p class="game-card-desc">Programmed by Doug Ross and John Ward on the transistorized MIT TX-0 computer. Draw labyrinth walls and cheese wedges on a vector display while a cybernetic mouse navigates the maze.</p>
    </div>
  </div>

  <!-- 52. SPACEWARS -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/spacewars.png" alt="Spacewars PDP-1 Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Spacewars</h3>
      <div class="game-card-subtitle">The 1962 MIT PDP-1 Vector Space Duel</div>
      <span class="game-card-badge">1962 CE • MIT</span>
      <p class="game-card-desc">Written by Steve Russell, Martin Graetz, and Wayne Wiitanen on DEC's PDP-1. Pilot the Wedge and Needle starships in zero-gravity orbital combat around a deadly gravitational star.</p>
    </div>
  </div>

  <!-- 53. BULLS & COWS -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/bulls_and_cows.png" alt="Bulls and Cows Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Bulls & Cows</h3>
      <div class="game-card-subtitle">Cambridge Deduction & Codebreaker</div>
      <span class="game-card-badge">1970 CE • Cambridge, UK</span>
      <p class="game-card-desc">The pencil-and-paper codebreaking game that inspired Mastermind. Guess a secret sequence—Bulls reveal correct numbers in exact positions, while Cows indicate numbers misplaced.</p>
    </div>
  </div>

  <!-- 54. MOON LANDER -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/moon_lander.png" alt="Moon Lander Vector CRT Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Moon Lander</h3>
      <div class="game-card-subtitle">The 1973 GT40 Apollo Vector Descent</div>
      <span class="game-card-badge">1973 CE • DEC (Jack Burness)</span>
      <p class="game-card-desc">Crafted by Jack Burness for DEC's GT40 vector display. Manage thruster burn, attitude angle, and fuel consumption to pilot the Apollo Lunar Module safely onto rugged lunar terrain.</p>
    </div>
  </div>

  <!-- 55. ARTILLERY -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/artillery.png" alt="Artillery Ballistic Duel Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Artillery</h3>
      <div class="game-card-subtitle">The 1970s Mountain Ballistic Duel</div>
      <span class="game-card-badge">1973–1976 CE • OMSI Portland</span>
      <p class="game-card-desc">The direct mechanical forefather of <em>Scorched Earth</em> and <em>Worms</em>. Calculate turret angle, propellant charge, and fluctuating crosswinds to land shells across procedural peaks.</p>
    </div>
  </div>

  <!-- 56. HUNT THE WUMPUS -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/hunt_the_wumpus.png" alt="Hunt the Wumpus Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Hunt the Wumpus</h3>
      <div class="game-card-subtitle">Gregory Yob's Dodecahedral Cavern Duel</div>
      <span class="game-card-badge">1973 CE • Dartmouth College</span>
      <p class="game-card-desc">A foundational early text adventure. Navigate 20 interconnected dodecahedral caves, detecting bottomless pits by drafts and bats by squeaks, before firing a crooked arrow into the beast's lair.</p>
    </div>
  </div>

  <!-- 57. FOUR IN A ROW -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/four_in_a_row.png" alt="Four in a Row Connect 4 Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Four in a Row</h3>
      <div class="game-card-subtitle">The Captain's Mistress: Vertical Drop</div>
      <span class="game-card-badge">1974 CE • Milton Bradley</span>
      <p class="game-card-desc">The beloved vertical disc drop classic. Players take turns slotting colored tokens into a 7x6 upright grid, racing to complete a four-in-a-row horizontally, vertically, or diagonally.</p>
    </div>
  </div>

  <!-- 58. BLOCKADE -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/blockade.png" alt="Blockade Board Game Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Blockade</h3>
      <div class="game-card-subtitle">Mirko Marchesi's Labyrinth Duel</div>
      <span class="game-card-badge">1975 CE • Italy</span>
      <p class="game-card-desc">The direct precursor to the modern classic <em>Quoridor</em>. Race pawns across the board while deploying wooden barrier walls to force your opponent into frustrating detours without fully blocking them.</p>
    </div>
  </div>

  <!-- 59. SNAKE -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/snake.png" alt="Snake Arcade Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Snake</h3>
      <div class="game-card-subtitle">The 1976 Arcade & Nokia Mobile Legend</div>
      <span class="game-card-badge">1976 CE • San Diego (Lane Hauck)</span>
      <p class="game-card-desc">Born in the 1976 Gremlin arcade game <em>Blockade</em> and played by billions on early Nokia handsets. Steer an ever-growing serpent to gobble pellets without crashing into walls or your own tail.</p>
    </div>
  </div>

  <!-- 60. GAME OF THE AMAZONS -->
  <div class="game-card">
    <div class="game-card-img-wrapper">
      <img src="/posts/images/game-archive/game_of_the_amazons.png" alt="Game of the Amazons Screenshot">
    </div>
    <div class="game-card-body">
      <h3 class="game-card-title">Game of the Amazons</h3>
      <div class="game-card-subtitle">Walter Zamkauskas's Flaming Arrow Duel</div>
      <span class="game-card-badge">1988 CE • Argentina</span>
      <p class="game-card-desc">Command 4 Amazon Queens on a 10x10 board. Queens slide in any orthogonal or diagonal direction, then fire a flaming arrow to permanently incinerate that square into an impassable block.</p>
    </div>
  </div>

</div>

---

## 🎯 Wrapping Up

Looking back at my post in August when I had only just got Chess and Ur working, it's pretty funny how much this grew. What started out as a pun turned into searching for and implementing 60 games from scratch, picking up the history behind them as I went, and finding a few new favorites along the way.

If you have some time and want to try any of them out, give the app a go!

<div class="cta-banner">
  <h3 style="margin: 0 0 8px 0; color: #fff;">Try Game Archive</h3>
  <p style="margin: 0; color: #cbd5e1; font-size: 0.95rem;">Available on Android • Free • 100% Offline</p>
  <a class="cta-button" href="https://play.google.com/store/apps/details?id=dev.matthewregis.bored_games" target="_blank" rel="noopener">📥 Download Free on Google Play</a>
</div>

*If there's an interesting classic or historical game you think I should add, feel free to let me know!*
