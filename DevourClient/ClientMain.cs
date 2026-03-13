using DevourClient.Helpers;
using MelonLoader;
using System.Threading.Tasks;
using Il2CppPhoton.Bolt;
using UnityEngine;
using Il2Cpp;

namespace DevourClient
{
    public class ClientMain : MonoBehaviour
    {
        public ClientMain(IntPtr ptr)
            : base(ptr)
        {
        }

        enum CurrentTab : int
        {
            Visuals = 0,
            Entities = 1,
            Map = 2,
            ESP = 3,
            Items = 4,
            Misc = 5,
            Players = 6
        }

        static Rect windowRect = new Rect(Settings.Settings.x + 10, Settings.Settings.y + 10, 800, 700);
        static CurrentTab current_tab = CurrentTab.Visuals;

        static bool flashlight_toggle = false;
        static bool flashlight_colorpick = false;
        static bool player_esp_colorpick = false;
        static bool azazel_esp_colorpick = false;
        static bool spoofLevel = false;
        static float spoofLevelValue = 0;
        static bool change_server_name = false;
        static bool change_steam_name = false;
        static bool fly = false;
        static float fly_speed = 5;
        static bool fastMove = false;
        static float _PlayerSpeedMultiplier = 1;
        public static float lobbySize = 4;
        public static bool _IsAutoRespawn = false;
        public static bool unlimitedUV = false;
        public static bool exp_modifier = false;
        public static float exp = 1000f;
        public static bool _walkInLobby = false;
        static bool player_esp = false;
        static bool player_skel_esp = false;
        static bool player_snapline = false;
        static bool azazel_esp = false;
        static bool azazel_skel_esp = false;
        static bool azazel_snapline = false;
        static bool spam_message = false;
        static bool item_esp = false;
        static bool goat_rat_esp = false;
        static bool demon_esp = false;
        static bool fullbright = false;
        static bool need_fly_reset = false;
        static bool crosshair = false;
        static bool in_game_cache = false;
        static bool should_show_start_message = true;
        static Texture2D crosshairTexture = default!;

        public void Start()
        {
            MelonLogger.Msg("Para a Rainha!");
            MelonLogger.Warning("Modificado e Atualizado por G4T0XX");
            MelonLogger.Warning("Atualização do Carnival integrada.");

            crosshairTexture = Helpers.GUIHelper.GetCircularTexture(5, 5);

            MelonCoroutines.Start(Helpers.Entities.GetLocalPlayer());
            MelonCoroutines.Start(Helpers.Entities.GetGoatsAndRats());
            MelonCoroutines.Start(Helpers.Entities.GetSurvivalInteractables());
            MelonCoroutines.Start(Helpers.Entities.GetKeys());
            MelonCoroutines.Start(Helpers.Entities.GetDemons());
            MelonCoroutines.Start(Helpers.Entities.GetSpiders());
            MelonCoroutines.Start(Helpers.Entities.GetGhosts());
            MelonCoroutines.Start(Helpers.Entities.GetBoars());
            MelonCoroutines.Start(Helpers.Entities.GetCorpses());
            MelonCoroutines.Start(Helpers.Entities.GetCrows());
            MelonCoroutines.Start(Helpers.Entities.GetLumps());
            MelonCoroutines.Start(Helpers.Entities.GetAzazels());
            MelonCoroutines.Start(Helpers.Entities.GetAllPlayers());
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Insert))
            {
                try
                {
                    Il2Cpp.GameUI gameUI = UnityEngine.Object.FindObjectOfType<Il2Cpp.GameUI>();
                    if (Settings.Settings.menu_enable)
                    {
                        gameUI.HideMouseCursor();
                    }
                    else
                    {
                        gameUI.ShowMouseCursor();
                    }
                }
                catch { }

                Settings.Settings.menu_enable = !Settings.Settings.menu_enable;
            }

            if (Player.IsInGame())
            {
                if (flashlight_toggle && !fullbright)
                {
                    Hacks.Misc.BigFlashlight(false);
                }
                else if (!flashlight_toggle && !fullbright)
                {
                    Hacks.Misc.BigFlashlight(true);
                }

                if (fullbright && !flashlight_toggle)
                {
                    Hacks.Misc.Fullbright(false);
                }
                else if (!fullbright && !flashlight_toggle)
                {
                    Hacks.Misc.Fullbright(true);
                }

                if (_IsAutoRespawn && Helpers.Player.IsPlayerCrawling())
                {
                    Hacks.Misc.AutoRespawn();
                }

                if (crosshair && !in_game_cache)
                {
                    in_game_cache = true;
                }
            }
            else
            {
                if (change_server_name)
                {
                    Hacks.Misc.SetServerName("G4T0XX no comando!");
                }

                if (change_steam_name)
                {
                    Hacks.Misc.SetSteamName("G4T0XX");
                }

                if (crosshair && in_game_cache)
                {
                    in_game_cache = false;
                }
            }

            if (spam_message)
            {
                Hacks.Misc.MessageSpam(Settings.Settings.message_to_spam);
            }

            if (spoofLevel)
            {
                Hacks.Misc.SetRank((int)spoofLevelValue);
            }

            if (Input.GetKeyDown(Settings.Settings.flyKey))
            {
                fly = !fly;
            }

            if (Player.IsInGameOrLobby())
            {
                if (fly && !need_fly_reset)
                {
                    Il2Cpp.NolanBehaviour nb = Player.GetPlayer();
                    if (nb)
                    {
                        Collider coll = nb.GetComponentInChildren<Collider>();
                        if (coll)
                        {
                            coll.enabled = false;
                            need_fly_reset = true;
                        }
                    }
                }

                else if (!fly && need_fly_reset)
                {
                    Il2Cpp.NolanBehaviour nb = Player.GetPlayer();
                    if (nb)
                    {
                        Collider coll = nb.GetComponentInChildren<Collider>();
                        if (coll)
                        {
                            coll.enabled = true;
                            need_fly_reset = false;
                        }
                    }
                }

                if (fly)
                {
                    Hacks.Misc.Fly(fly_speed);
                }

            }

            if (Helpers.Map.GetActiveScene() == "Menu")
            {
                Hacks.Misc.WalkInLobby(_walkInLobby);
            }

            if (fastMove)
            {
                try
                {
                    Helpers.Entities.LocalPlayer_.p_GameObject.GetComponent<Il2CppOpsive.UltimateCharacterController.Character.UltimateCharacterLocomotion>().TimeScale = _PlayerSpeedMultiplier;
                }
                catch { return; }
            }
        }

        public void OnGUI()
        {
            if (should_show_start_message)
            {
                if (DevourClient.Hacks.Misc.ShowMessageBox("Bem-vindo ao Menu do G4T0XX.\n\nAperte a tecla INS (Insert) para abrir o menu.") == 0)
                    should_show_start_message = false;
            }

            GUI.backgroundColor = Color.grey;

            GUI.skin.button.normal.background = GUIHelper.MakeTex(2, 2, Color.black);
            GUI.skin.button.normal.textColor = Color.white;

            GUI.skin.button.hover.background = GUIHelper.MakeTex(2, 2, Color.green);
            GUI.skin.button.hover.textColor = Color.black;

            GUI.skin.toggle.onNormal.textColor = Color.yellow;

            if (UnityEngine.Event.current.type == EventType.Repaint)
            {
                if (player_esp || player_snapline || player_skel_esp)
                {
                    foreach (Helpers.BasePlayer p in Helpers.Entities.Players)
                    {
                        if (p == null) continue;
                        GameObject player = p.p_GameObject;
                        if (player != null)
                        {
                            Il2Cpp.NolanBehaviour nb = player.GetComponent<Il2Cpp.NolanBehaviour>();
                            if (nb.entity.IsOwner) continue;

                            if (player_skel_esp)
                            {
                                Render.Render.DrawAllBones(Hacks.Misc.GetAllBones(nb.animator), Settings.Settings.player_esp_color);
                            }
                            Render.Render.DrawBoxESP(player, -0.25f, 1.75f, p.Name, Settings.Settings.player_esp_color, player_snapline, player_esp);
                        }
                    }
                }

                if (goat_rat_esp)
                {
                    foreach (Il2Cpp.GoatBehaviour goat in Helpers.Entities.GoatsAndRats)
                    {
                        if (goat != null)
                        {
                            Render.Render.DrawNameESP(goat.transform.position, goat.name.Replace("Survival", "").Replace("(Clone)", ""), new Color(0.94f, 0.61f, 0.18f, 1.0f));
                        }
                    }
                }

                if (item_esp)
                {
                    foreach (Il2Cpp.SurvivalInteractable obj in Helpers.Entities.SurvivalInteractables)
                    {
                        if (obj != null)
                        {
                            Render.Render.DrawNameESP(obj.transform.position, obj.prefabName.Replace("Survival", ""), new Color(1.0f, 1.0f, 1.0f));
                        }
                    }

                    foreach (Il2Cpp.KeyBehaviour key in Helpers.Entities.Keys)
                    {
                        if (key != null)
                        {
                            Render.Render.DrawNameESP(key.transform.position, "Key", new Color(1.0f, 1.0f, 1.0f));
                        }
                    }
                }

                if (demon_esp)
                {
                    foreach (Il2Cpp.SurvivalDemonBehaviour demon in Helpers.Entities.Demons)
                    {
                        if (demon != null) Render.Render.DrawNameESP(demon.transform.position, demon.name.Replace("Survival", "").Replace("(Clone)", ""), new Color(1.0f, 0.0f, 0.0f, 1.0f));
                    }
                    foreach (Il2Cpp.SpiderBehaviour spider in Helpers.Entities.Spiders)
                    {
                        if (spider != null) Render.Render.DrawNameESP(spider.transform.position, "Spider", new Color(1.0f, 0.0f, 0.0f, 1.0f));
                    }
                    foreach (Il2Cpp.GhostBehaviour ghost in Helpers.Entities.Ghosts)
                    {
                        if (ghost != null) Render.Render.DrawNameESP(ghost.transform.position, "Ghost", new Color(1.0f, 0.0f, 0.0f, 1.0f));
                    }
                    foreach (Il2Cpp.BoarBehaviour boar in Helpers.Entities.Boars)
                    {
                        if (boar != null) Render.Render.DrawNameESP(boar.transform.position, "Boar", new Color(1.0f, 0.0f, 0.0f, 1.0f));
                    }
                    foreach (Il2Cpp.CorpseBehaviour corpse in Helpers.Entities.Corpses)
                    {
                        if (corpse != null) Render.Render.DrawNameESP(corpse.transform.position, "Corpse", new Color(1.0f, 0.0f, 0.0f, 1.0f));
                    }
                    foreach (Il2Cpp.CrowBehaviour crow in Helpers.Entities.Crows)
                    {
                        if (crow != null) Render.Render.DrawNameESP(crow.transform.position, "Crow", new Color(1.0f, 0.0f, 0.0f, 1.0f));
                    }
                    foreach (Il2Cpp.ManorLumpController lump in Helpers.Entities.Lumps)
                    {
                        if (lump != null) Render.Render.DrawNameESP(lump.transform.position, "Lump", new Color(1.0f, 0.0f, 0.0f, 1.0f));
                    }
                }

                if (azazel_esp || azazel_snapline || azazel_skel_esp)
                {
                    foreach (Il2Cpp.SurvivalAzazelBehaviour survivalAzazel in Helpers.Entities.Azazels)
                    {
                        if (survivalAzazel != null)
                        {
                            if (azazel_skel_esp)
                            {
                                Render.Render.DrawAllBones(Hacks.Misc.GetAllBones(survivalAzazel.animator), Settings.Settings.azazel_esp_color);
                            }
                            Render.Render.DrawBoxESP(survivalAzazel.gameObject, -0.25f, 2.0f, "Azazel/Kai", Settings.Settings.azazel_esp_color, azazel_snapline, azazel_esp);
                        }
                    }
                }

                if (crosshair && in_game_cache)
                {
                    const float crosshairSize = 4;
                    float xMin = (Settings.Settings.width) - (crosshairSize / 2);
                    float yMin = (Settings.Settings.height) - (crosshairSize / 2);

                    if (crosshairTexture == null)
                    {
                        crosshairTexture = Helpers.GUIHelper.GetCircularTexture(5, 5);
                    }

                    GUI.DrawTexture(new Rect(xMin, yMin, crosshairSize, crosshairSize), crosshairTexture);
                }
            }

            if (Settings.Settings.menu_enable)
            {
                windowRect = GUI.Window(0, windowRect, (GUI.WindowFunction)Tabs, "G4T0XX | DEVOUR ULTIMATE V4.2");
            }
        }

        public static void Tabs(int windowID)
        {
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Visual", GUILayout.Height(40)) || Input.GetKeyDown(KeyCode.F1)) current_tab = CurrentTab.Visuals;
            if (GUILayout.Button("Entidades", GUILayout.Height(40)) || Input.GetKeyDown(KeyCode.F2)) current_tab = CurrentTab.Entities;
            if (GUILayout.Button("Mapa", GUILayout.Height(40)) || Input.GetKeyDown(KeyCode.F3)) current_tab = CurrentTab.Map;
            if (GUILayout.Button("Visualização (ESP)", GUILayout.Height(40)) || Input.GetKeyDown(KeyCode.F4)) current_tab = CurrentTab.ESP;
            if (GUILayout.Button("Itens", GUILayout.Height(40)) || Input.GetKeyDown(KeyCode.F5)) current_tab = CurrentTab.Items;
            if (GUILayout.Button("Diversos", GUILayout.Height(40)) || Input.GetKeyDown(KeyCode.F6)) current_tab = CurrentTab.Misc;
            if (GUILayout.Button("Jogadores", GUILayout.Height(40)) || Input.GetKeyDown(KeyCode.F7)) current_tab = CurrentTab.Players;

            GUILayout.EndHorizontal();

            switch (current_tab)
            {
                case CurrentTab.Visuals: VisualsTab(); break;
                case CurrentTab.Entities: EntitiesTab(); break;
                case CurrentTab.Map: MapSpecificTab(); break;
                case CurrentTab.ESP: EspTab(); break;
                case CurrentTab.Items: ItemsTab(); break;
                case CurrentTab.Misc: MiscTab(); break;
                case CurrentTab.Players: PlayersTab(); break;
            }

            GUI.DragWindow();
        }

        private static void VisualsTab()
        {
            flashlight_toggle = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 70, 150, 30), flashlight_toggle, "Lanterna Forte");
            fullbright = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 100, 150, 30), fullbright, "Brilho Máximo (Fullbright)");
            unlimitedUV = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 130, 150, 30), unlimitedUV, "Luz UV Infinita");
            crosshair = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 160, 150, 30), crosshair, "Mira na Tela");

            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 190, 130, 30), "Cor da Lanterna"))
            {
                flashlight_colorpick = !flashlight_colorpick;
            }

            if (flashlight_colorpick)
            {
                Color flashlight_color_input = DevourClient.Helpers.GUIHelper.ColorPick("Cor da Lanterna", Settings.Settings.flashlight_color);
                Settings.Settings.flashlight_color = flashlight_color_input;

                if (Player.IsInGame()) Hacks.Misc.FlashlightColor(flashlight_color_input);
            }
        }

        private static void EntitiesTab()
        {
            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 70, 160, 30), "Trazer itens até você (TP)"))
            {
                Hacks.Misc.TPItems();
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 110, 160, 30), "Congelar Azazel"))
            {
                Hacks.Misc.FreezeAzazel();
            }

            GUI.Label(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 150, 160, 30), "Azazel e Demônios");

            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 180, 60, 25), "Sam") && Player.IsInGameOrLobby() && BoltNetwork.IsServer) Hacks.Misc.SpawnAzazel((PrefabId)BoltPrefabs.AzazelSam);
            if (GUI.Button(new Rect(Settings.Settings.x + 80, Settings.Settings.y + 180, 60, 25), "Molly") && Player.IsInGameOrLobby() && BoltNetwork.IsServer) Hacks.Misc.SpawnAzazel((PrefabId)BoltPrefabs.SurvivalAzazelMolly);
            if (GUI.Button(new Rect(Settings.Settings.x + 150, Settings.Settings.y + 180, 60, 25), "Anna") && Player.IsInGameOrLobby() && BoltNetwork.IsServer) BoltNetwork.Instantiate(BoltPrefabs.SurvivalAnnaNew, Player.GetPlayer().transform.position, Quaternion.identity);
            if (GUI.Button(new Rect(Settings.Settings.x + 220, Settings.Settings.y + 180, 60, 25), "Zara") && Player.IsInGameOrLobby() && BoltNetwork.IsServer) BoltNetwork.Instantiate(BoltPrefabs.AzazelZara, Player.GetPlayer().transform.position, Quaternion.identity);
            if (GUI.Button(new Rect(Settings.Settings.x + 290, Settings.Settings.y + 180, 60, 25), "Nathan") && Player.IsInGameOrLobby() && BoltNetwork.IsServer) BoltNetwork.Instantiate(BoltPrefabs.AzazelNathan, Player.GetPlayer().transform.position, Quaternion.identity);
            if (GUI.Button(new Rect(Settings.Settings.x + 360, Settings.Settings.y + 180, 60, 25), "April") && Player.IsInGameOrLobby() && BoltNetwork.IsServer) BoltNetwork.Instantiate(BoltPrefabs.AzazelApril, Player.GetPlayer().transform.position, Quaternion.identity);

            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 220, 80, 25), "Fantasma") && Player.IsInGameOrLobby() && BoltNetwork.IsServer) BoltNetwork.Instantiate(BoltPrefabs.Ghost, Player.GetPlayer().transform.position, Quaternion.identity);
            if (GUI.Button(new Rect(Settings.Settings.x + 100, Settings.Settings.y + 220, 80, 25), "Prisioneiro") && Player.IsInGameOrLobby() && BoltNetwork.IsServer) BoltNetwork.Instantiate(BoltPrefabs.SurvivalInmate, Player.GetPlayer().transform.position, Quaternion.identity);
            if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 220, 80, 25), "Demônio") && Player.IsInGameOrLobby() && BoltNetwork.IsServer) BoltNetwork.Instantiate(BoltPrefabs.SurvivalDemon, Player.GetPlayer().transform.position, Quaternion.identity);
            if (GUI.Button(new Rect(Settings.Settings.x + 280, Settings.Settings.y + 220, 80, 25), "Javali") && Player.IsInGameOrLobby() && BoltNetwork.IsServer) BoltNetwork.Instantiate(BoltPrefabs.Boar, Player.GetPlayer().transform.position, Quaternion.identity);
            if (GUI.Button(new Rect(Settings.Settings.x + 370, Settings.Settings.y + 220, 80, 25), "Cadáver") && BoltNetwork.IsServer && Player.IsInGameOrLobby()) BoltNetwork.Instantiate(BoltPrefabs.Corpse, Player.GetPlayer().transform.position, Quaternion.identity);
            if (GUI.Button(new Rect(Settings.Settings.x + 460, Settings.Settings.y + 220, 80, 25), "Corvo") && BoltNetwork.IsServer && Player.IsInGameOrLobby()) BoltNetwork.Instantiate(BoltPrefabs.Crow, Player.GetPlayer().transform.position, Quaternion.identity);
            if (GUI.Button(new Rect(Settings.Settings.x + 550, Settings.Settings.y + 220, 80, 25), "Gosma") && BoltNetwork.IsServer && Player.IsInGameOrLobby()) BoltNetwork.Instantiate(BoltPrefabs.ManorLump, Player.GetPlayer().transform.position, Quaternion.identity);

            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 260, 60, 25), "Rato"))
            {
                if (BoltNetwork.IsServer && !Player.IsInGame()) BoltNetwork.Instantiate(BoltPrefabs.SurvivalRat, Player.GetPlayer().transform.position, Quaternion.identity);
                if (Player.IsInGame() && !Player.IsPlayerCrawling()) Hacks.Misc.CarryObject("SurvivalRat");
            }
            if (GUI.Button(new Rect(Settings.Settings.x + 80, Settings.Settings.y + 260, 60, 25), "Cabra"))
            {
                if (BoltNetwork.IsServer && !Player.IsInGame()) BoltNetwork.Instantiate(BoltPrefabs.SurvivalGoat, Player.GetPlayer().transform.position, Quaternion.identity);
                if (Player.IsInGame() && !Player.IsPlayerCrawling()) Hacks.Misc.CarryObject("SurvivalGoat");
            }
            if (GUI.Button(new Rect(Settings.Settings.x + 150, Settings.Settings.y + 260, 60, 25), "Aranha") && BoltNetwork.IsServer && Player.IsInGameOrLobby()) BoltNetwork.Instantiate(BoltPrefabs.Spider, Player.GetPlayer().transform.position, Quaternion.identity);
            if (GUI.Button(new Rect(Settings.Settings.x + 220, Settings.Settings.y + 260, 60, 25), "Porco"))
            {
                if (BoltNetwork.IsServer && !Player.IsInGame()) BoltNetwork.Instantiate(BoltPrefabs.SurvivalPig, Player.GetPlayer().transform.position, Quaternion.identity);
                if (Player.IsInGame() && !Player.IsPlayerCrawling()) Hacks.Misc.CarryObject("SurvivalPig");
            }    
        }

        private static void MapSpecificTab()
        {
            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 70, 160, 30), "Vitória Instantânea") && Player.IsInGame() && BoltNetwork.IsSinglePlayer)
            {
                Hacks.Misc.InstantWin();
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 110, 160, 30), "Queimar um item de ritual")) Hacks.Misc.BurnRitualObj(Helpers.Map.GetActiveScene(), false);
            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 150, 160, 30), "Queimar todos itens de ritual")) Hacks.Misc.BurnRitualObj(Helpers.Map.GetActiveScene(), true);

            switch (Helpers.Map.GetActiveScene())
            {
                case "Menu":
                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 70, 160, 30), "Forçar Início da Partida") && BoltNetwork.IsServer && !Player.IsInGame())
                    {
                        Il2CppHorror.Menu menu = UnityEngine.Object.FindObjectOfType<Il2CppHorror.Menu>();
                        menu.OnLobbyStartButtonClick();
                    }
                    break;

                case "Devour":
                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 70, 160, 30), "Teleporte para Azazel"))
                    {
                        try { Player.GetPlayer().TeleportTo(Helpers.Map.GetAzazel().transform.position, Quaternion.identity); } catch { }
                    }
                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 110, 160, 30), "Remover Demônios")) Hacks.Misc.DespawnDemons();
                    break;

                case "Molly":
                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 70, 160, 30), "Teleporte para Azazel"))
                    {
                        try { Player.GetPlayer().TeleportTo(Helpers.Map.GetAzazel().transform.position, Quaternion.identity); } catch { }
                    }
                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 110, 160, 30), "Remover Prisioneiros")) Hacks.Misc.DespawnDemons();
                    break;

                case "Inn":
                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 70, 160, 30), "Teleporte para Azazel"))
                    {
                        try { Player.GetPlayer().TeleportTo(Helpers.Map.GetAzazel().transform.position, Quaternion.identity); } catch { }
                    }
                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 110, 160, 30), "Limpar as Fontes")) Hacks.Misc.CleanFountain();
                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 150, 160, 30), "Remover Aranhas")) Hacks.Misc.DespawnSpiders();
                    break;

                case "Town":
                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 70, 160, 30), "Teleporte para Azazel"))
                    {
                        try { Player.GetPlayer().TeleportTo(Helpers.Map.GetAzazel().transform.position, Quaternion.identity); } catch { }
                    }
                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 110, 160, 30), "Remover Fantasmas")) Hacks.Misc.DespawnGhosts();
                    break;

                case "Slaughterhouse":
                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 70, 160, 30), "Teleporte para Azazel"))
                    {
                        try { Player.GetPlayer().TeleportTo(Helpers.Map.GetAzazel().transform.position, Quaternion.identity); } catch { }
                    }
                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 110, 160, 30), "Remover Javalis")) Hacks.Misc.DespawnBoars();
                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 150, 160, 30), "Remover Cadáveres")) Hacks.Misc.DespawnCorpses();
                    break;

                case "Manor":
                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 70, 160, 30), "Teleporte para Azazel"))
                    {
                        try { Player.GetPlayer().TeleportTo(Helpers.Map.GetAzazel().transform.position, Quaternion.identity); } catch { }
                    }
                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 110, 160, 30), "Remover Corvos")) Hacks.Misc.DespawnCrows();
                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 150, 160, 30), "Remover Gosmas")) Hacks.Misc.DespawnLumps();
                    break;

                case "Carnival":
                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 70, 160, 30), "Teleporte para Kai (Chefe)"))
                    {
                        try { Player.GetPlayer().TeleportTo(Helpers.Map.GetAzazel().transform.position, Quaternion.identity); } catch { MelonLogger.Msg("Kai não encontrado!"); }
                    }
                    if (GUI.Button(new Rect(Settings.Settings.x + 190, Settings.Settings.y + 110, 160, 30), "Remover Macacos")) MelonLogger.Msg("Opção de remover macacos precisa ser ativada no Misc.cs no futuro.");
                    break;
            }

            GUI.Label(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 210, 100, 30), "Carregar Mapa: ");

            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 240, 100, 30), "Farmhouse") && BoltNetwork.IsServer) Helpers.Map.LoadMap("Devour");
            if (GUI.Button(new Rect(Settings.Settings.x + 120, Settings.Settings.y + 240, 100, 30), "Asylum") && BoltNetwork.IsServer) Helpers.Map.LoadMap("Molly");
            if (GUI.Button(new Rect(Settings.Settings.x + 230, Settings.Settings.y + 240, 100, 30), "Inn") && BoltNetwork.IsServer) Helpers.Map.LoadMap("Inn");
            if (GUI.Button(new Rect(Settings.Settings.x + 340, Settings.Settings.y + 240, 100, 30), "Town") && BoltNetwork.IsServer) Helpers.Map.LoadMap("Town");
            if (GUI.Button(new Rect(Settings.Settings.x + 450, Settings.Settings.y + 240, 100, 30), "Slaughterhouse") && BoltNetwork.IsServer) Helpers.Map.LoadMap("Slaughterhouse");
            if (GUI.Button(new Rect(Settings.Settings.x + 560, Settings.Settings.y + 240, 100, 30), "Manor") && BoltNetwork.IsServer) Helpers.Map.LoadMap("Manor");
            
            // --- CARNIVAL ADDED HERE ---
            if (GUI.Button(new Rect(Settings.Settings.x + 670, Settings.Settings.y + 240, 100, 30), "Carnival") && BoltNetwork.IsServer) Helpers.Map.LoadMap("Carnival");
        }

        private static void EspTab()
        {
            player_esp = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 70, 150, 20), player_esp, "ESP Jogador");
            player_skel_esp = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 100, 150, 20), player_skel_esp, "ESP Esqueleto");
            player_snapline = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 130, 150, 20), player_snapline, "Linha até Jogador");
            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 160, 130, 30), "Cor ESP Jogador")) player_esp_colorpick = !player_esp_colorpick;

            if (player_esp_colorpick)
            {
                Color player_esp_color_input = GUIHelper.ColorPick("Cor ESP Jogador", Settings.Settings.player_esp_color);
                Settings.Settings.player_esp_color = player_esp_color_input;
            }

            azazel_esp = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 200, 150, 20), azazel_esp, "ESP Azazel/Kai");
            azazel_skel_esp = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 230, 150, 20), azazel_skel_esp, "ESP Esqueleto");
            azazel_snapline = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 260, 160, 20), azazel_snapline, "Linha até Azazel/Kai");
            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 290, 130, 30), "Cor ESP Azazel")) azazel_esp_colorpick = !azazel_esp_colorpick;

            if (azazel_esp_colorpick)
            {
                Color azazel_esp_color_input = GUIHelper.ColorPick("Cor ESP Azazel", Settings.Settings.azazel_esp_color);
                Settings.Settings.azazel_esp_color = azazel_esp_color_input;
            }

            item_esp = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 330, 150, 20), item_esp, "ESP Itens");
            goat_rat_esp = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 360, 150, 20), goat_rat_esp, "ESP Cabra/Rato");
            demon_esp = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 390, 150, 20), demon_esp, "ESP Demônio/Macaco");
        }

        private static void ItemsTab()
        {
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            GUILayout.Label("Itens Normais");

            Settings.Settings.itemsScrollPosition = GUILayout.BeginScrollView(Settings.Settings.itemsScrollPosition, GUILayout.Width(220), GUILayout.Height(190));

            if (GUILayout.Button("Feno")) Hacks.Misc.CarryObject("SurvivalHay");
            if (GUILayout.Button("Kit Médico")) Hacks.Misc.CarryObject("SurvivalFirstAid");
            if (GUILayout.Button("Bateria")) Hacks.Misc.CarryObject("SurvivalBattery");
            if (GUILayout.Button("Gasolina")) Hacks.Misc.CarryObject("SurvivalGasoline");
            if (GUILayout.Button("Fusível")) Hacks.Misc.CarryObject("SurvivalFuse");
            if (GUILayout.Button("Comida Podre")) Hacks.Misc.CarryObject("SurvivalRottenFood");
            if (GUILayout.Button("Osso")) Hacks.Misc.CarryObject("SurvivalBone");
            if (GUILayout.Button("Água Sanitária")) Hacks.Misc.CarryObject("SurvivalBleach");
            if (GUILayout.Button("Fósforos")) Hacks.Misc.CarryObject("Matchbox-3");
            if (GUILayout.Button("Pá")) Hacks.Misc.CarryObject("SurvivalSpade");
            if (GUILayout.Button("Bolo")) Hacks.Misc.CarryObject("SurvivalCake");

            GUILayout.EndScrollView();
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            GUILayout.Label("Objetos de Ritual (Inc. Carnival)");

            Settings.Settings.rituelObjectsScrollPosition = GUILayout.BeginScrollView(Settings.Settings.rituelObjectsScrollPosition, GUILayout.Width(220), GUILayout.Height(190));

            if (GUILayout.Button("Ingresso (Carnival)")) Hacks.Misc.CarryObject("SurvivalTicket");
            if (GUILayout.Button("Moeda (Carnival)")) Hacks.Misc.CarryObject("SurvivalCoin");
            if (GUILayout.Button("Cabeça de Boneca")) Hacks.Misc.CarryObject("SurvivalDollHead");

            if (GUILayout.Button("Ovo-1")) Hacks.Misc.CarryObject("Egg-Clean-1");
            if (GUILayout.Button("Livro de Ritual")) Hacks.Misc.CarryObject("RitualBook-Active-1");
            if (GUILayout.Button("Cabeça Suja")) Hacks.Misc.CarryObject("SurvivalHead");
            if (GUILayout.Button("Cabeça Limpa")) Hacks.Misc.CarryObject("SurvivalCleanHead");

            GUILayout.EndScrollView();
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            GUILayout.Label("Gerar Objetos");

            Settings.Settings.stuffsScrollPosition = GUILayout.BeginScrollView(Settings.Settings.stuffsScrollPosition, GUILayout.Width(220), GUILayout.Height(190));
            if (GUILayout.Button("Lixeira") && BoltNetwork.IsServer) BoltNetwork.Instantiate(BoltPrefabs.TrashCan, Player.GetPlayer().transform.position, Quaternion.identity);
            
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        private static void MiscTab()
        {
            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 70, 160, 30), "Desbloquear Conquistas"))
            {
                Thread AchievementsThread = new Thread(new ThreadStart(Hacks.Unlock.Achievements));
                AchievementsThread.Start();
            }

            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 110, 160, 30), "Destrancar Portas")) Hacks.Unlock.Doors();
            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 150, 160, 30), "Trazer Chaves (TP)") && Player.IsInGame()) Hacks.Misc.TPKeys();
            if (GUI.Button(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 190, 160, 30), "Fazer Barulho Aleatório")) Hacks.Misc.PlaySound();

            spam_message = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 240, 160, 30), spam_message, "Spam no Chat");
            change_steam_name = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 270, 160, 30), change_steam_name, "Mudar Nome na Steam");
            change_server_name = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 300, 160, 30), change_server_name, "Mudar Nome do Servidor");
            _walkInLobby = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 330, 160, 30), _walkInLobby, "Andar no Lobby");
            _IsAutoRespawn = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 360, 160, 30), _IsAutoRespawn, "Reviver Automático");

            fly = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 400, 50, 20), fly, "Voar");
            if (GUI.Button(new Rect(Settings.Settings.x + 60, Settings.Settings.y + 400, 40, 20), Settings.Settings.flyKey.ToString())) Settings.Settings.flyKey = Settings.Settings.GetKey();

            fly_speed = GUI.HorizontalSlider(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 430, 100, 10), fly_speed, 5f, 20f);
            GUI.Label(new Rect(Settings.Settings.x + 120, Settings.Settings.y + 425, 100, 30), ((int)fly_speed).ToString());

            spoofLevel = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 470, 150, 20), spoofLevel, "Forjar Nível");
            spoofLevelValue = GUI.HorizontalSlider(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 500, 100, 10), spoofLevelValue, 0f, 666f);
            GUI.Label(new Rect(Settings.Settings.x + 120, Settings.Settings.y + 495, 100, 30), ((int)spoofLevelValue).ToString());

            exp_modifier = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 540, 150, 20), exp_modifier, "Modificador de EXP");
            exp = GUI.HorizontalSlider(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 570, 100, 10), exp, 1000f, 3000f);
            GUI.Label(new Rect(Settings.Settings.x + 120, Settings.Settings.y + 565, 100, 30), ((int)exp).ToString());

            fastMove = GUI.Toggle(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 610, 150, 20), fastMove, "Velocidade do Jogador");
            _PlayerSpeedMultiplier = GUI.HorizontalSlider(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 640, 100, 10), _PlayerSpeedMultiplier, (int)1f, (int)10f);
            GUI.Label(new Rect(Settings.Settings.x + 120, Settings.Settings.y + 635, 100, 30), ((int)_PlayerSpeedMultiplier).ToString());

            GUI.Label(new Rect(Settings.Settings.x + 300, Settings.Settings.y + 70, 150, 30), "Máx. Jogadores");
            lobbySize = GUI.HorizontalSlider(new Rect(Settings.Settings.x + 300, Settings.Settings.y + 90, 100, 10), lobbySize, (int)0f, (int)30f);
            GUI.Label(new Rect(Settings.Settings.x + 410, Settings.Settings.y + 85, 100, 30), ((int)lobbySize).ToString());

            if (GUI.Button(new Rect(Settings.Settings.x + 290, Settings.Settings.y + 110, 150, 30), "Criar Servidor"))
            {
                Hacks.Misc.CreateCustomizedLobby((int)lobbySize);
            }
        }

        private static void PlayersTab()
        {
            if (Helpers.Map.GetActiveScene() != "Menu")
            {
                GUI.Label(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 70, 150, 30), "Lista de Jogadores:");
                int i = 0;
                foreach (BasePlayer bp in Entities.Players)
                {
                    if (bp == null || bp.Name == "") continue;

                    GUI.Label(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 110 + i, 150, 30), bp.Name);
                    if (GUI.Button(new Rect(Settings.Settings.x + 160, Settings.Settings.y + 105 + i, 60, 30), "Matar")) bp.Kill();
                    if (GUI.Button(new Rect(Settings.Settings.x + 230, Settings.Settings.y + 105 + i, 65, 30), "Reviver")) bp.Revive();
                    if (GUI.Button(new Rect(Settings.Settings.x + 305, Settings.Settings.y + 105 + i, 70, 30), "Assustar")) bp.Jumpscare();
                    if (GUI.Button(new Rect(Settings.Settings.x + 385, Settings.Settings.y + 105 + i, 80, 30), "Ir Até (TP)")) bp.TP();
                    if (GUI.Button(new Rect(Settings.Settings.x + 475, Settings.Settings.y + 105 + i, 120, 30), "Prender na Jaula")) bp.LockInCage();
                    if (GUI.Button(new Rect(Settings.Settings.x + 605, Settings.Settings.y + 105 + i, 100, 30), "Trazer Azazel")) bp.TPAzazel();

                    if (Helpers.Map.GetActiveScene() == "Town")
                    {
                        if (GUI.Button(new Rect(Settings.Settings.x + 715, Settings.Settings.y + 105 + i, 120, 30), "Atirar no Jogador")) bp.ShootPlayer();
                    }
                    i += 30;
                }
            }
            else
            {
                GUI.Label(new Rect(Settings.Settings.x + 10, Settings.Settings.y + 70, 200, 30), "Aguardando o jogo começar.");
            }
        }
    }
}