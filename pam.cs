

/*//////////////////////////
 * Thank you for using:
 * [PAM] - Path Auto Miner
 * ————————————
 * Author:  Keks
 * Last update: 2019-12-20
 * ————————————
 * Guide: https://steamcommunity.com/sharedfiles/filedetails/?id=1553126390
 * Script: https://steamcommunity.com/sharedfiles/filedetails/?id=1507646929
 * Youtube: https://youtu.be/ne_i5U2Y8Fk
 * ————————————
 * Please report bugs here:
 * https://steamcommunity.com/workshop/filedetails/discussion/1507646929/2727382174640941895/
 * ————————————
 * I hope you enjoy this script and don't forget
 * to leave a comment on the steam workshop
 *//////////////////////////

const string VERSION = "1.3.1";
const string DATAREV = "14";

const String pamTag = "[PAM]";
const String controllerTag = "[PAM-Controller]";
//Tag for LCD's of cockpits and other blocks: [PAM:<lcdIndex>] e.g: [PAM:1]

const int gyroSpeedSmall = 15; //[RPM] small ship
const int gyroSpeedLarge = 5; //[RPM] large ship
const int generalSpeedLimit = 100; //[m/s] 0 = no limit (if set to 0 ignore unreachable code warning)
const float dockingSpeed = 0.5f; //[m/s]

//multiplied with ship size
const float dockDist = 0.6f; //position in front of the home connector
const float followPathDock = 2f; //stop following path, fly direct to target
const float followPathJob = 1f; //same as dock
const float useDockDirectionDist = 1f; //Override waypoint direction, use docking dir
const float useJobDirectionDist = 0f; //same as dock

//other distances
const float wpReachedDist = 2f;//[m]
const float drillRadius = 1.4f;//[m]

//grinding
const float sensorRange = 2f;//fly slow when blocks found in this range
const float fastSpeed = 10f;//speed when no blocks are detected

//minimum acceleration in space before ship becomes too heavy
const float minAccelerationSmall = 0.2f;//[m/s²] small ship
const float minAccelerationLarge = 0.1f;//[m/s²] large ship

//stone ejection
const float minEjection = 25f;//[%] Min amount of ejected cargo to continue job

//LCD
const bool setLCDFontAndSize = true;

//Check if blocks are connected with conveyors
const bool checkConveyorSystem = false;//temporarily disabled because of a SE bug


public String GetElementCode(String itemName)
{
    //Here you can define custom element codes for the PAM-Controller
    //You can extend this when you are using mods which adds new materials
    //This is not necessary for any function of PAM, it is just a little detail on the controller screen
    //It only needs to be changed in the controller pb
    switch (itemName)
    {
        case "IRON": return "Fe";
        case "NICKEL": return "Ni";
        case "COBALT": return "Co";
        case "MAGNESIUM": return "Mg";
        case "SILICON": return "Si";
        case "SILVER": return "Ag";
        case "GOLD": return "Au";
        case "PLATINUM": return "Pt";
        case "URANIUM": return "U";

        //add new entries here

        //example:
        //New material: ExampleOre
        //Element code: Ex

        //case "EXAMPLEORE": return "Ex";

        default: return ""; //don't change this!
    }
}

Program()
{
    /**
     * Original: χ => GTS
     */
    GTS = GridTerminalSystem;
    Runtime.UpdateFrequency = UpdateFrequency.Update10;
    if (ї(Me, controllerTag, true)) ƽ = Enum13.А; Љ(Enum1.Ϸ);
    if (ƽ != Enum13.А) {
        G_VAR12 = "Welcome to [PAM]!";
        Enum16 ϊ = ɖ();
        if (ƽ == Enum13.ǻ) {
            List<IMyShipDrill> ω = new List<IMyShipDrill>();
            List<IMyShipGrinder> ψ = new List<IMyShipGrinder>();
            GTS.GetBlocksOfType(ω, q => q.CubeGrid == Me.CubeGrid);
            GTS.GetBlocksOfType(ψ, q => q.CubeGrid == Me.CubeGrid);
            if (ω.Count > 0) {
                ƽ = Enum13.Б;
                G_VAR12 = "Miner mode enabled!";
            } else if (ψ.Count > 0) {
                ƽ = Enum13.ψ;
                G_VAR12 = "Grinder mode enabled!";
            } else {
                ƽ = Enum13.Ͼ;
                Ͻ(Enum2.Ͼ);
            }
        }

        if (ϊ == Enum16.ə) ν = false;
        if (ϊ == Enum16.ɘ) G_VAR12 = "Data restore failed!";
        if (ϊ == Enum16.Ə) G_VAR12 = "New version, wipe data";
    }
}

IMyGridTerminalSystem GTS;
Vector3 φ = new Vector3();
Vector3 ɉ = new Vector3();
Vector3 υ = new Vector3();
Vector3 τ = new Vector3();
Vector3 σ = new Vector3();
DateTime ς = new DateTime();
bool ρ = true;
int π = 0;
bool ο = false;
bool ξ = false;
bool ν = true;
bool ƿ = false;
bool μ = false;
bool λ = false;
float κ = 0;
float ϋ = 0;
int ι = 0;
int Ə = 0;
int Ϊ = 0;
int Ω = 0;
float Ψ = 0;
float Χ = 0;
double Φ = 0;
float Υ = 0;
List<int> Τ = new List<int>();
List<int> Σ = new List<int>();

void Main(string Ƹ, UpdateType Ρ) {
    try {
        if (ȥ != null) {
            Ҕ();
            Ȥ();
            return;
        }

        ξ = (Ρ & UpdateType.Update10) != 0;

        if (ξ)
            π++;

        ο = π >= 10;

        if (ο)
            π = 0;

        if (ξ) {
            G_VAR8++;
            if (G_VAR8 > 4)
                G_VAR8 = 0;

            Ϊ = Math.Max(0, 10 - (DateTime.Now - ς).Seconds);
        }

        if (Ƹ != "")
            ȿ = Ƹ;

        if (ƽ != Enum13.А)
            Λ(Ƹ);
        else
            ǌ(Ƹ);

        G_VAR4 = false;

        try {
            int Ο = Runtime.CurrentInstructionCount;
            float A = µ(Ο, Runtime.MaxInstructionCount);
            if (A > 0.90)
                G_VAR12 = "Max. instructions >90%";

            if (A > Ψ)
                Ψ = A;

            if (G_VAR1) {
                Τ.Add(Ο);
                while (Τ.Count > 10)
                    Τ.RemoveAt(0);

                Χ = 0;
                for (int ã = 0; ã < Τ.Count; ã++) {
                    Χ += Τ[ã];
                }

                Χ = µ(µ(Χ, Τ.Count), Runtime.MaxInstructionCount);
                double Ξ = Runtime.LastRunTimeMs;
                if (λ && Ξ > Φ)
                    Φ = Ξ;

                Σ.Add((int)(Ξ * 1000f));
                while (Σ.Count > 10)
                    Σ.RemoveAt(0);

                Υ = 0;
                for (int ã = 0; ã < Σ.Count; ã++) {
                    Υ += Σ[ã];
                }
                Υ = µ(Υ, Σ.Count) / 1000f;
            }
        }
        catch {
            Ψ = 0;
        }
    }
    catch (Exception e) {
        ȥ = e;
    }
}

/**
 * Original: Ν
 */
bool G_VAR1 = false;

/**
 * Original: Μ
 */
bool G_VAR2 = false;

void Λ(string Ƹ) {
    bool Π = false;
    String Κ = "";
    if (ɘ <= 1 && !Ⱦ(Ƹ))
        η(Ƹ);

    if (ɬ != null && ɬ.HasPendingMessage) {
        MyIGCMessage ǉ = ɬ.AcceptMessage();
        String ž = (string)ǉ.Data;
        String ǋ = "";
        if (ɘ <= 1 && Ȼ(ref ž, out ǋ, out Κ) && ǋ == ɟ) {
            η(ž); Π = true;
        }
    }

    bool θ = λ && G_VAR68_Enum10 == Enum10.Д && !μ && !Π && ι == 0 && !G_VAR49_bool;
    if (ο && G_VAR68_Enum10 != Enum10.Д)
        Π = true;

    if ((ξ && !θ) || (ο && θ)) {
        if (ι == 0 && (Ϊ <= 0 || ρ)) {
            ƿ = ɘ > 0;
            ɘ = 0;
            ι = 1;
            ô();
            Ѹ();
            ɪ();
            ó("Scan 1");
        } else if (ι == 1) {
            ι = 2;
            ô();
            Ҁ();
            ó("Scan 2");
        } else if (ι == 2) {
            ι = 0;
            ô();
            Ѻ();
            ó("Scan 3");
            ς = DateTime.Now;
            if (ɘ <= 1 && ν)
                φ = Ȓ(Э, Э.CenterOfMass);

            ν = false;
            if (ρ) {
                ρ = false;
                Ҕ();
            }
            if (ƿ && ɘ == 0)
                G_VAR12 = "Setup complete";
        }
        else {
            if (G_VAR61_Enum9 == Enum9.Ӊ && ƽ != Enum13.Ͼ) {
                ô();
                ѓ();
                ó("Inv balance");
            }

            ô();
            switch (Ə) {
                case 0:
                    Ħ();
                    break;

                case 1:
                    ħ();
                    break;

                case 2:
                    İ();
                    break;

                case 3:
                    ķ();
                    break;

                case 4:
                    Ļ();
                    break;

                case 5:
                    ĳ();
                    break;

                case 6:
                    D(Э);
                    break;
            }

            ó("Update: " + Ə);
            Ə++;
            if (Ə > 6) {
                Ə = 0;
                λ = true;
                if (ɛ != Enum9.Ӌ) {
                    switch (ɛ) {
                        case Enum9.Ӈ:
                            қ();
                            break;

                        case Enum9.ӆ:
                            қ();
                            break;

                        case Enum9.Ӊ:
                            қ();
                            break;

                        case Enum9.ғ:
                            ҏ();
                            break;

                        case Enum9.ӈ:
                            Ґ();
                            break;
                    }
                    ɛ = Enum9.Ӌ;
                }
            }
        }

        if (!ρ) {
            if (!Ƃ(Э, true)) {
                Э = null;
                ν = true;
                ɘ = 2;
            }
            if (ɘ >= 2 && G_VAR68_Enum10 != Enum10.Д)
                Ҕ();

            if (ɘ <= 1) {
                ϋ = Э.CalculateShipMass().PhysicalMass;
                κ = (float)Э.GetShipSpeed();
                ɉ = Ȕ(Э, φ);
                υ = Э.WorldMatrix.Forward;
                τ = Э.WorldMatrix.Left;
                σ = Э.WorldMatrix.Down;
                ˇ();
                if (G_VAR68_Enum10 != Enum10.Д) {
                    μ = false;
                    ű(false);
                    È(false);
                    String O = ϕ(G_VAR68_Enum10) + " " + (int)G_VAR68_Enum10;
                    ô();
                    ӗ();
                    З(false);
                    ó(O);
                    ô();
                    ą();
                    ó("Thruster");
                    ô();
                    Ĕ();
                    ó("Gyroscope");
                } else {
                    if (μ) {
                        if (ù()) {
                            ë(σ, υ, τ, 0.25f, true);
                            Ĕ();
                            G_VAR12 = "Aligning to planet: " + Math.Round(ė - 0.25f, 2) + "°";
                            if (ě)
                                α(true, true);
                        } else
                            α(true, true);
                    }
                }
                G_VAR2 = false;
            }
        }
    }
    ô();
    ȟ();
    ó("Print");
    if (Π || Ω <= 0) {
        ô();
        ȸ(Κ);
        ó("Broadcast");
        Ω = 4;
    } else if (ο)
        Ω--;
}

void η(string Ƹ) {
    if (Ƹ == "")
        return;

    var ª = Ƹ.ToUpper().Split(' ');
    ª.DefaultIfEmpty("");
    var Ƽ = ª.ElementAtOrDefault(0);
    var Ʒ = ª.ElementAtOrDefault(1);
    var ζ = ª.ElementAtOrDefault(2);
    var ε = ª.ElementAtOrDefault(3);
    String ǆ = "Invalid argument: " + Ƹ;
    bool δ = false;

    switch (Ƽ) {
        case "UP":
            this.Ј(false);
            break;

        case "DOWN":
            this.Ї(false);
            break;

        case "UPLOOP":
            this.Ј(true);
            break;

        case "DOWNLOOP":
            this.Ї(true);
            break;

        case "APPLY":
            this.ϣ(true);
            break;

        case "MRES":
            G_VAR6 = 0;
            break;

        case "STOP":
            this.Ҕ();
            break;

        case "PATHHOME":
            {
                this.Ҕ();
                this.ʹ();
            }
            break;
        case "PATH":
            {
                this.Ҕ();
                this.ʹ();
                G_VAR43_Class1.Ͷ = true;
            }
            break;

        case "START":
            {
                this.Ҕ();
                Ұ();
            }
            break;

        case "ALIGN":
            {
                α(!μ, false);
            }
            break;

        case "CONT":
            {
                this.Ҕ();
                this.қ();
            }
            break;

        case "JOBPOS":
            {
                this.Ҕ();
                this.Ґ();
            }
            break;

        case "HOMEPOS":
            {
                this.Ҕ();
                this.ҏ();
            }
            break;

        case "FULL":
            {
                Ɔ = true;
            }
            break;

        case "RESET":
            {
                ɜ = true;
                ɘ = 2;
            }
            break;

        default:
            δ = true;
            break;
    }

    if (ƽ != Enum13.Ͼ) {
        switch (Ƽ) {
            case "SHUTTLE":
                {
                    β();
                }
                break;

            case "CFGS":
                {
                    if (!ί(Ʒ, ζ, ε)) G_VAR12 = ǆ;
                }
                break;

            case "CFGB":
                {
                    if (!Ϲ(Ʒ, ζ))
                        G_VAR12 = ǆ;
                }
                break;

            case "CFGL":
                {
                    if (!ʈ(ref G_VAR34_float, true, Enum7.Ă, Ʒ, "") || !Ϻ(ζ))
                        G_VAR12 = ǆ;
                }
                break;

            case "CFGE":
                {
                    if (!ʈ(ref G_VAR36_float, true, Enum7.ʏ, Ʒ, "IG") || !ʈ(ref G_VAR35_float, true, Enum7.ʐ, ζ, "IG") || !ʈ(ref G_VAR37_float, true, Enum7.ʎ, ε, "IG"))
                        G_VAR12 = ǆ;
                }
                break;

            case "CFGA":
                {
                    if (!ʈ(ref G_VAR38_float, false, Enum7.ª, Ʒ, ""))
                        G_VAR12 = ǆ;
                }
                break;

            case"CFGW":
                {
                    if (!ʈ(ref G_VAR39_float, false, Enum7.ʓ, Ʒ, "") || !ʈ(ref G_VAR40_float, false, Enum7.ʓ, ζ, ""))
                        G_VAR12 = ǆ;
                }
                break;

            case "NEXT":
                {
                    Ά(false);
                }
                break;

            case "PREV":
                {
                    Ά(true);
                }
                break;

            default:
                if (δ)
                    G_VAR12 = ǆ;

                break;
        }
    } else {
        switch (Ƽ) {
            case "UNDOCK":
                {
                    G_VAR2 = true;
                }
                break;

            default:
                if (δ)
                    G_VAR12 = ǆ;
                break;
        }
    }
}

String γ() {
    String O = "\n\n" + 
        "Run-arguments: (Type without:[ ])\n" + 
        "———————————————\n" + 
        "[UP] Menu navigation up\n" + 
        "[DOWN] Menu navigation down\n" +
        "[APPLY] Apply menu point\n\n" +
        "[UPLOOP] \"UP\" + looping\n" + 
        "[DOWNLOOP] \"DOWN\" + looping\n" + 
        "[PATHHOME] Record path, set home\n" +
        "[PATH] Record path, use old home\n" + 
        "[START] Start job\n" + 
        "[STOP] Stop every process\n" + 
        "[CONT] Continue last job\n" + 
        "[JOBPOS] Move to job position\n" +
        "[HOMEPOS] Move to home position\n\n" + 
        "[FULL] Simulate ship is full\n" + 
        "[ALIGN] Align the ship to planet\n" + 
        "[RESET] Reset all data\n";

    if (ƽ != Enum13.Ͼ)
        O += "[SHUTTLE] Enable shuttle mode\n" + 
            "[NEXT] Next hole\n" + 
            "[PREV] Previous hole\n\n" + 
            "[CFGS width height depth]*\n" + 
            "[CFGB done damage]*\n" +
            "[CFGL maxload weightLimit]*\n" + 
            "[CFGE minUr minBat minHyd]*\n" + 
            "[CFGW forward backward]*\n" + 
            "[CFGA acceleration]*\n" + 
            "———————————————\n" +
            "*[CFGS] = Config Size:\n" + 
            " e.g.: \"CFGS 5 3 20\"\n\n" + 
            "*[CFGB] = Config Behaviour:\n" + 
            " When done: [HOME,STOP]\n" +
            " On Damage: [HOME,JOB,STOP,IG]\n" + 
            " e.g.: \"CFGB HOME IG\"\n\n" + 
            "*[CFGL] = Config max load:\n" + 
            " maxload: [10..95]\n" + 
            " weight limit: [On/Off]\n" +
            " e.g.: \"CFGL 70 on\"\n\n" +
            "*[CFGE] = Config energy:\n" + 
            " minUr (Uranium): [1..25, IG]\n" + 
            " minBat (Battery): [5..30, IG]\n" +
            " minHyd (Hydrogen): [10..90, IG]\n" +
            " e.g.: \"CFGE 20 10 IG\"\n\n" +
            "*[CFGW] = Config work speed:\n" + 
            " fwd: [0.5..10]\n" + 
            " bwd: [0.5..10]\n" +
            " e.g.: \"CFGW 1.5 2\"\n\n" + 
            "*[CFGA] = Config acceleration:\n" + 
            " acceleration: [10..100]\n" + 
            " e.g.: \"CFGA 80\"\n";
    else
        O +="[UNDOCK] Leave current connector\n\n";

    return O;
}

void β() {
    Ҕ();
    ƽ = Enum13.Ͼ;
    Ͻ(Enum2.Ͼ);
    G_VAR58_Class1.Ͷ = false;
    G_VAR43_Class1.Ͷ = false;
    Ь = null;
    Ш.Clear();
    G_VAR61_Enum9 = Enum9.Ӌ;
}

void α(bool ά, bool ΰ)
{
    if (!ά)
        G_VAR12 = "Aligning canceled";

    if (ΰ)
        G_VAR12 = "Aligning done";

    if (ΰ || !ά) {
        μ = false;
        ì();
        á(false, 0, 0, 0, 0);
        return;
    }

    if (ù())
        μ = true;
}

bool ί(String ή, String έ, String Ϋ) {
    bool ά = G_VAR61_Enum9 == Enum9.Ӊ;
    int Y, X, ț;
    if (int.TryParse(ή, out Y) && int.TryParse(έ, out X) && int.TryParse(Ϋ, out ț)) {
        this.Ҕ();
        G_VAR24_int = Y;
        G_VAR25_int = X;
        G_VAR26_int = ț;
        ʸ(false);
        Қ(false, false);
        if (ά)
            қ();

        return true;
    }

    return false;
}

bool Ϻ(String ʓ) {
    if (ʓ == "ON") {
        G_VAR28_bool = true;
        return true;
    }

    if (ʓ == "OFF") {
        G_VAR28_bool = false;
        return true;
    }

    return false;
}

bool Ϲ(String ț, String Ʀ) {
    bool J = true;
    if (ț == "HOME")
        G_VAR22_bool = true;
    else if (ț == "STOP")
        G_VAR22_bool = false;
    else
        J = false;

    if (Ʀ == "HOME")
        G_VAR21_Enum5 = Enum5.ʮ;
    else if (Ʀ == "STOP")
        G_VAR21_Enum5 = Enum5.ʬ;
    else if (Ʀ == "JOB")
        G_VAR21_Enum5 = Enum5.ʭ;
    else if (Ʀ == "IG")
        G_VAR21_Enum5 = Enum5.ʫ;
    else
        J = false;

    return J;
}

/**
 * Original: ϸ
 */
public enum Enum1 {
    Ϸ,
    ϵ,
    ϴ,
    ϳ,
    ϲ,
    ϱ,
    ϰ,
    ϯ,
    Ϯ,
    ϭ,
    Ϭ,
    Ƿ,
    ž,
    ϻ,
    ϼ,
    Ў,
    Џ,
    Ѝ,
    Ќ
}

/**
 * Original: Ћ
 */
int[] G_VAR3 = new int[Enum.GetValues(Enum1.ž.GetType()).Length];

/**
 * Original: Њ
 */
bool G_VAR4 = false;

void Љ(Enum1 ʻ) {
    G_VAR3[(int)G_VAR7] = G_VAR6;
    G_VAR6 = G_VAR3[(int)ʻ];
    if (ʻ == Enum1.Ϭ)
        G_VAR6 = 0;

    G_VAR7 = ʻ;
    if (ƽ != Enum13.А)
        Ş(G_VAR7 == Enum1.ϴ, false, 0, 0);

    G_VAR4 = true;
}

void Ј(bool ʹ) {
    if (G_VAR6 > 0)
        G_VAR6--;
    else if (ʹ)
        G_VAR6 = G_VAR5 - 1;
}

void Ї(bool ʹ) {
    if (G_VAR6 < G_VAR5 - 1)
        G_VAR6++;
    else if (ʹ)
        G_VAR6 = 0;
}

/**
 * Original: І
 */
int G_VAR5 = 0;

/**
 * Original: Ѕ
 */
int G_VAR6 = 0;

/**
 * Original: Є
 */
Enum1 G_VAR7 = Enum1.Ϸ;

/**
 * Original: Ѓ 
 */ 
public enum Enum2 {
    Ђ,
    Ё,
    Ѐ,
    Ͽ,
    Ͼ
}

String Ͻ(Enum2 ƿ) {
    switch (ƿ) {
        case Enum2.Ђ:
            G_VAR12 = "Job is running";
            break;

        case Enum2.Ё:
            G_VAR12 = "Connector not ready!";
            break;

        case Enum2.Ѐ:
            G_VAR12 = "Ship modified, path outdated!";
            break;

        case Enum2.Ͽ:
            G_VAR12 = "Interrupted by player!";
            break;

        case Enum2.Ͼ:
            G_VAR12 = "Shuttle mode enabled!";
            break;
    }

    return "";
}

String ϝ(Enum4 ϫ) {
    switch (ϫ) {
        case Enum4.ʱ:
            return "Top-Left";

        case Enum4.ʰ:
            return "Center";

        default:
            return "";
    }
}

String ϝ(Enum3 Ϫ) {
    switch (Ϫ) {
        case Enum3.ʴ:
            return "Auto" + (ƽ == Enum13.Б ? " (Ore)" : "");

        case Enum3.ʳ:
            return "Auto (+Stone)";

        case Enum3.ʵ:
            return "Default";

        default: return "";
    }
}

String Ϝ(Enum9 ϛ) {
    switch (ϛ) {
        case Enum9.Ӌ:
            return "No job";

        case Enum9.ӊ:
            return "Job paused";

        case Enum9.Ӊ:
            return "Job active";

        case Enum9.Ӈ:
            return "Job active";

        case Enum9.ӆ:
            return "Job active";

        case Enum9.ΰ:
            return "Job done";

        case Enum9.Ғ:
            return "Job changed";

        case Enum9.ғ:
            return "Move home";

        case Enum9.ӈ:
            return "Move to job";
    }

    return "";
}

String Ϛ(Enum5 ϙ) {
    switch (ϙ) {
        case Enum5.ʮ:
            return "Return home";

        case Enum5.ʭ:
            return "Fly to job pos";

        case Enum5.ʬ:
            return "Stop";

        case Enum5.ʫ:
            return "Ignore";
    }

    return "";
}

String Ϙ(Enum6 ș) {
    switch (ș) {
        case Enum6.ɗ:
            return "Off";

        case Enum6.ʧ:
            return "Drop pos (Stone) ";

        case Enum6.ʦ:
            return "Drop pos (Sto.+Ice)";

        case Enum6.ʩ:
            return "Cur. pos (Stone)";

        case Enum6.ʨ:
            return "Cur. pos (Sto.+Ice)";

        case Enum6.ʥ:
            return "In motion (Stone)";

        case Enum6.ʺ:
            return "In motion (Sto.+Ice)";
    }

    return "";
}

String ϗ(Enum12 ϖ) {
    switch (ϖ) {
        case Enum12.ɗ:
            return "No batteries";

        case Enum12.Ŧ:
            return "Charging";

        case Enum12.Г:
            return "Discharging";
    }

    return "";
}

String ϕ(Enum10 ϛ) {
    String O = ƽ == Enum13.Ͼ ? "target" : "job";
    switch (ϛ) {
        case Enum10.Д:
            return "Idle";

        case Enum10.ʟ:
            return "Flying to XY position";

        case Enum10.Ҍ:
            return ƽ == Enum13.ψ ? "Grinding" : "Mining";

        case Enum10.ҝ:
            return "Returning";

        case Enum10.ң:
            return "Flying to drop pos";

        case Enum10.ұ:
            return "Returning to dock";

        case Enum10.ү:
            return "Flying to dock area";

        case Enum10.Ү:
            return "Flying to job area";

        case Enum10.Ҧ:
            return "Flying to path";

        case Enum10.ҭ:
            return "Flying to job position";

        case Enum10.Ҭ:
            return "Approaching dock";

        case Enum10.ґ:
            return "Docking";

        case Enum10.ҫ:
            return "Aligning";

        case Enum10.Ҫ:
            return "Aligning";

        case Enum10.ҩ:
            return "Retry docking";

        case Enum10.Ҩ:
            return "Unloading";

        case Enum10.ҡ:
            return Ɗ;

        case Enum10.ҧ:
            return "Undocking";

        case Enum10.Ŧ:
            return "Charging batteries";

        case Enum10.Ĺ:
            return "Waiting for uranium";

        case Enum10.ļ:
            return "Filling up hydrogen";
        case Enum10.ҥ:
            return "Waiting for ejection";

        case Enum10.Ҥ:
            return "Waiting for ejection";

        case Enum10.Ң:
            return "Flying to drop pos";
    }
    return "";
}

String ϓ(Enum15 Ų) {
    switch (Ų) {
        case Enum15.ž:
            return "On \"Undock\" command";

        case Enum15.Ž:
            return "On player entered cockpit";

        case Enum15.Ż:
            return "On ship is full";

        case Enum15.ź:
            return "On ship is empty";

        case Enum15.ż:
            return "On time delay";

        case Enum15.Ÿ:
            return "On batteries empty(<25%)";

        case Enum15.ŷ:
            return "On batteries empty(=0%)";

        case Enum15.Ź:
            return "On batteries full";

        case Enum15.ŵ:
            return "On hydrogen empty(<25%)";

        case Enum15.Ŵ:
            return "On hydrogen empty(=0%)";

        case Enum15.Ŷ:
            return "On hydrogen full";
    }

    return "";
}

/**
 * Original: ϒ
 */
int G_VAR8 = 0;

/**
 * Original: ϑ
 */
int G_VAR9 = 0;

/**
 * Original: ϐ
 */
int G_VAR10 = 0;

/**
 * Original: Ϗ
 */
int G_VAR11 = 0;

/**
 * Original: ώ
 * Looks like a status message.
 */
String G_VAR12 = "";

bool ύ(ref String J, int ϔ, int ό, bool ƛ, String ū) {
    G_VAR5 += 1;
    if (ϔ == ό)
        ū = ">" + ū + (G_VAR8 >= 2 ? " ." : "");
    else
        ū = " " + ū;

    J += ū + "\n";
    return ϔ == ό && ƛ;
}

/**
 * Original: ϩ
 */
int G_VAR13 = 0;

/**
 * Original: Ϩ
 */
int G_VAR14 = 0;

/**
 * Original: ϧ
 */
int G_VAR15 = 0;

/**
 * Original: Ϧ
 */
int G_VAR16 = 0;

/**
 * Original: ϥ 
 */
int G_VAR17 = 0;

/**
 * Original: Ϥ
 */
int G_VAR18 = 0;

String ϣ(bool ƛ) {
    int A = 0;
    int ƴ = G_VAR6;
    G_VAR5 = 0;
    String Ʃ = "———————————————\n";
    String ƶ = "--------------------------------------------\n";
    String Ɨ = "";
    Ɨ += Ϝ(G_VAR61_Enum9) + " | " + (G_VAR43_Class1.Ͷ ? "Ready to dock" : "No dock") + "\n";
    Ɨ += Ʃ;

    double Ϣ = Math.Max(Math.Round(this.Ԍ), 0);
    if (G_VAR7 == Enum1.Ϸ) {
        bool O = ƽ == Enum13.Ͼ;
        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Record path & set home"))
            ʹ();

        if (ƽ == Enum13.Б)
            if (ύ(ref Ɨ, ƴ, A++, ƛ, " Setup mining job"))
                Љ(Enum1.ϴ);

        if (ƽ == Enum13.ψ)
            if (ύ(ref Ɨ, ƴ, A++, ƛ, " Setup grinding job"))
                Љ(Enum1.ϴ);

        if (ƽ == Enum13.Ͼ)
            if (ύ(ref Ɨ, ƴ, A++, ƛ, " Setup shuttle job"))
                Љ(Enum1.Ѝ);

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Continue job"))
            қ();

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Fly to home position"))
            ҏ();

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Fly to job position"))
            Ґ();

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Behavior settings"))
            if (O)
                Љ(Enum1.Џ);
            else
                Љ(Enum1.ϱ);

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Info"))
            Љ(Enum1.ϯ);

        if (ƽ != Enum13.Ͼ)
            if (ύ(ref Ɨ, ƴ, A++, ƛ, " Help"))
                Љ(Enum1.ϭ);
    } else if (G_VAR7 == Enum1.ϴ) {
        double ϡ = Math.Round(G_VAR24_int * Й, 1);
        double Ϡ = Math.Round(G_VAR25_int * Я, 1);
        String Ι = "";
        if (ύ(ref Ι, ƴ, A++, ƛ, " Start new job!"))
            Ұ();

        if (ύ(ref Ι, ƴ, A++, ƛ, " Change current job")) {
            Қ(false, false);
            Љ(Enum1.Ϸ);
        }

        if (ύ(ref Ι, ƴ, A++, ƛ, " Width + (Width: " + G_VAR24_int + " = " + ϡ + "m)")) {
            ő(ref G_VAR24_int, 5, 20, 1);
            ʸ(true);
        }

        if (ύ(ref Ι, ƴ, A++, ƛ, " Width -")) {
            ő(ref G_VAR24_int, -5, 20, -1);
            ʸ(true);
        }

        if (ύ(ref Ι, ƴ, A++, ƛ, " Height + (Height: " + G_VAR25_int + " = " + Ϡ + "m)")) {
            ő(ref G_VAR25_int, 5, 20, 1);
            ʸ(true);
        }

        if (ύ(ref Ι, ƴ, A++, ƛ, " Height -")) {
            ő(ref G_VAR25_int, -5, 20, -1);
            ʸ(true);
        }

        if (ύ(ref Ι, ƴ, A++, ƛ, " Depth + (" + (G_VAR27_Enum3 == Enum3.ʵ ? "Depth" : "Min") + ": " + G_VAR26_int + "m)")) {
            ő(ref G_VAR26_int, 5, 50, 2);
            ʸ(true);
        }

        if (ύ(ref Ι, ƴ, A++, ƛ, " Depth -")) {
            ő(ref G_VAR26_int, -5, 50, -2);
            ʸ(true);
        }

        if (ύ(ref Ι, ƴ, A++, ƛ," Depth mode: " + ϝ(G_VAR27_Enum3))) {
            G_VAR27_Enum3 = ͼ(G_VAR27_Enum3);
        }

        if (ύ(ref Ι, ƴ, A++, ƛ, " Start pos: " + ϝ(G_VAR23_Enum4))) {
            G_VAR23_Enum4 = ͼ(G_VAR23_Enum4);
        }

        if (ƽ == Enum13.ψ && G_VAR27_Enum3 == Enum3.ʳ)
            G_VAR27_Enum3 = ͼ(G_VAR27_Enum3);

        Ɨ += ǧ(8, Ι, ƴ, ref G_VAR17);
    } else if (G_VAR7 == Enum1.Ѝ) {
        float[] ʜ = new float[] { 0, 3, 10, 30, 60, 300, 600, 1200, 1800 };
        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Next")) {
            Љ(Enum1.Ќ);
        }

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Back")) {
            Љ(Enum1.Ϸ);
        }

        Ɨ += " Leave connector 1:\n";
        if (ύ(ref Ɨ, ƴ, A++, ƛ, " - " + ϓ(G_VAR19_Class5.Ų)))
            G_VAR19_Class5.Ų = ͼ(G_VAR19_Class5.Ų);

        if (!G_VAR19_Class5.Ǝ())
            Ɨ += "\n";
        else if (ύ(ref Ɨ, ƴ, A++, ƛ, " - Delay: " + Ǖ((int)G_VAR19_Class5.Ƅ)))
            G_VAR19_Class5.Ƅ = ʞ(G_VAR19_Class5.Ƅ, ʜ);

        Ɨ += " Leave connector 2:\n";
        if (ύ(ref Ɨ, ƴ, A++, ƛ, " - " + ϓ(G_VAR20_Class5.Ų)))
            G_VAR20_Class5.Ų = ͼ(G_VAR20_Class5.Ų);

        if (!G_VAR20_Class5.Ǝ())
            Ɨ += "\n";
        else if (ύ(ref Ɨ, ƴ, A++, ƛ, " - Delay: " + Ǖ((int)G_VAR20_Class5.Ƅ)))
            G_VAR20_Class5.Ƅ = ʞ(G_VAR20_Class5.Ƅ, ʜ);
    }
    else if (G_VAR7 == Enum1.Ќ) {
        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Start job!"))
            Ұ();

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Back")) {
            Љ(Enum1.Ѝ);
        }

        Ɨ += " Timer: \"Docking connector 1\":\n";
        if (ύ(ref Ɨ, ƴ, A++, ƛ, " = " + (G_VAR19_Class5.Ɣ != "" ? G_VAR19_Class5.Ɣ: "-")))
            G_VAR19_Class5.Ɣ = ʙ(ref G_VAR13);

        Ɨ += " Timer: \"Leaving connector 1\":\n";
        if (ύ(ref Ɨ, ƴ, A++, ƛ, " = " + (G_VAR19_Class5.ƕ != "" ? G_VAR19_Class5.ƕ : "-")))
            G_VAR19_Class5.ƕ = ʙ(ref G_VAR15);

        Ɨ += " Timer: \"Docking connector 2\":\n";
        if (ύ(ref Ɨ, ƴ, A++, ƛ, " = " + (G_VAR20_Class5.Ɣ != "" ? G_VAR20_Class5.Ɣ : "-")))
            G_VAR20_Class5.Ɣ = ʙ(ref G_VAR14);

        Ɨ += " Timer: \"Leaving connector 2\":\n";
        if (ύ(ref Ɨ, ƴ, A++, ƛ, " = " + (G_VAR20_Class5.ƕ != "" ? G_VAR20_Class5.ƕ : "-")))
            G_VAR20_Class5.ƕ = ʙ(ref G_VAR16);
    }
    else if (G_VAR7 == Enum1.ϳ) {
        String ϟ = G_VAR34_float + " %";
        if (ο)
            G_VAR9++;

        if (G_VAR9 > 1) {
            G_VAR9 = 0;
            G_VAR10++;
            if (G_VAR10 > 1)
                G_VAR10 = 0;

            bool[] Ϟ = new bool[]{Ц.Count==0,ĥ==Enum12.ɗ,Ф.Count==0};
            int Ä = 0;
            while (true) {
                Ä++;
                G_VAR11++;
                if (G_VAR11 > Ϟ.Length - 1)
                    G_VAR11 = 0;

                if (Ä >= Ϟ.Length)
                    break;

                if (!Ϟ[G_VAR11])
                    break;
            }
        }

        bool O = ƽ == Enum13.Ͼ;
        if (!O && G_VAR28_bool && Ƈ != -1 && G_VAR10 == 0)
            ϟ = Ƈ < 1000000 ? Math.Round(Ƈ) + " Kg" : Math.Round(Ƈ / 1000) + " t";

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Stop!")) {
            Ҕ();
            Љ(Enum1.Ϸ);
        }

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Behavior settings"))
            if (!O)
                Љ(Enum1.ϱ);
            else
                Љ(Enum1.Џ);

        if (!O) {
            if (ύ(ref Ɨ, ƴ, A++, ƛ, " Next hole"))
                Ά(false);
        } else if (ύ(ref Ɨ, ƴ, A++, ƛ, " Undock"))
            G_VAR2 = true;

        Ɨ += ƶ;
        if (!O)
            Ɨ += "Progress: " + Math.Round(G_VAR65_double, 1) + " %\n";

        Ɨ += "State: " + ϕ(G_VAR68_Enum10) + " " + Ϣ + "m \n";
        Ɨ += "Load: " + ŏ + " % Max: " + ϟ + " \n";
        if (G_VAR11 == 0)
            Ɨ += "Uranium: " + (Ц.Count == 0 ? "No reactors" : Math.Round(Ĺ, 1) + "Kg " + (G_VAR36_float == -1 ? "" : " Min: " + G_VAR36_float + " Kg")) + "\n";

        if (G_VAR11 == 1)
            Ɨ += "Battery: " + (ĥ == Enum12.ɗ ? ϗ(ĥ) : Ĩ + "% " + (G_VAR35_float == -1 || O ? "" : " Min: " + G_VAR35_float + " %")) + "\n";

        if (G_VAR11 == 2)
            Ɨ += "Hydrogen: " + (Ф.Count == 0 ? "No tanks" : Math.Round(ļ, 1) + "% " + (G_VAR37_float == -1 || O ? "" : " Min: " + G_VAR37_float + " %")) + "\n";
    } else if (G_VAR7 == Enum1.ϱ) {
        String Ι = "";
        if (ύ(ref Ι, ƴ, A++, ƛ, " Back")) {
            if (G_VAR61_Enum9 == Enum9.Ӊ)
                Љ(Enum1.ϳ);
            else
                Љ(Enum1.Ϸ);
        }

        if (ύ(ref Ι, ƴ, A++, ƛ, " Max load: " + G_VAR34_float + "%"))
            ʈ(ref G_VAR34_float, G_VAR34_float <= 80 ? -10 : -5, Enum7.Ă, false);

        if (ύ(ref Ι, ƴ, A++, ƛ, " Weight limit: " + (G_VAR28_bool ? "On" : "Off")))
            G_VAR28_bool = !G_VAR28_bool;

        if (ύ(ref Ι, ƴ, A++, ƛ, " Ejection: " + Ϙ(G_VAR32_Enum6))) {
            G_VAR32_Enum6 = ͼ(G_VAR32_Enum6);
        }

        if (ύ(ref Ι, ƴ, A++, ƛ, " Toggle sorters: " + (G_VAR31_bool ? "On" : "Off"))) {
            G_VAR31_bool = !G_VAR31_bool;
            if (G_VAR31_bool)
                Ũ(ũ);
        }

        if (ύ(ref Ι, ƴ, A++, ƛ, " Unload ice: " + (G_VAR33_bool ? "On" : "Off")))
            G_VAR33_bool = !G_VAR33_bool;

        if (ύ(ref Ι, ƴ, A++, ƛ, " Uranium: " + (G_VAR36_float == -1 ? "Ignore" : "Min " + G_VAR36_float + "Kg")))
            ʈ(ref G_VAR36_float, (G_VAR36_float > 5 ? -5 : -1), Enum7.ʏ, true);

        if (ύ(ref Ι, ƴ, A++, ƛ, " Battery: " + (G_VAR35_float == -1 ? "Ignore" : "Min " + G_VAR35_float + "%")))
            ʈ(ref G_VAR35_float, -5, Enum7.ʐ, true);

        if (ύ(ref Ι, ƴ, A++, ƛ, " Hydrogen: " + (G_VAR37_float == -1 ? "Ignore" : "Min " + G_VAR37_float + "%")))
            ʈ(ref G_VAR37_float, -10, Enum7.ʎ, true);

        if (ύ(ref Ι, ƴ, A++, ƛ, " When done: " + (G_VAR22_bool ? "Return home" : "Stop")))
            G_VAR22_bool = !G_VAR22_bool;

        if (ύ(ref Ι, ƴ, A++, ƛ, " On damage: " + Ϛ(G_VAR21_Enum5))) {
            G_VAR21_Enum5 = ͼ(G_VAR21_Enum5);
        }

        if (ύ(ref Ι, ƴ, A++, ƛ, " Advanced..."))
            Љ(Enum1.ϰ);

        Ɨ += ǧ(8, Ι, ƴ, ref G_VAR18);
    } else if (G_VAR7 == Enum1.ϰ) {
        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Back")) {
            if (G_VAR61_Enum9 == Enum9.Ӊ)
                Љ(Enum1.ϳ);
            else
                Љ(Enum1.Ϸ);
        }

        if (ύ(ref Ɨ, ƴ, A++, ƛ, (ƽ == Enum13.ψ ? " Grinder" : " Drill") + " inv. balancing: " + (G_VAR29_bool ? "On" : "Off")))
            G_VAR29_bool = !G_VAR29_bool;

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Enable" + (ƽ == Enum13.ψ ? " grinders" : " drills") + ": " + (G_VAR30_bool ? "Fwd + Bwd" : "Fwd")))
            G_VAR30_bool = !G_VAR30_bool;

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Work speed fwd.: " + G_VAR39_float + "m/s"))
            ʈ(ref G_VAR39_float, 0.5f, Enum7.ʓ, false);

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Work speed bwd.: " + G_VAR40_float + "m/s"))
            ʈ(ref G_VAR40_float, 0.5f, Enum7.ʓ, false);

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Acceleration: " + Math.Round(G_VAR38_float * 100f) + "%" + (G_VAR38_float > 0.80f ? " (risky)" : ""))) {
            ʈ(ref G_VAR38_float, 0.1f, Enum7.ª, false);
        }

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Width overlap: " + G_VAR41_float * 100f + "%"))
            ʠ(true, 0.05f);

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Height overlap: " + G_VAR42_float * 100f + "%"))
            ʠ(false, 0.05f);
    } else if (G_VAR7 == Enum1.Џ) {
        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Back")) {
            if (G_VAR61_Enum9 == Enum9.Ӊ)
                Љ(Enum1.ϳ);
            else
                Љ(Enum1.Ϸ);
        }

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Max load: " + G_VAR34_float + "%"))
            ʈ(ref G_VAR34_float, G_VAR34_float <= 80 ? -10 : -5, Enum7.Ă, false);

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Unload ice: " + (G_VAR33_bool ? "On" : "Off")))
            G_VAR33_bool = !G_VAR33_bool;

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Uranium: " + (G_VAR36_float == -1 ? "Ignore" : "Min " + G_VAR36_float + "Kg")))
            ʈ(ref G_VAR36_float, (G_VAR36_float > 5 ? -5 : -1), Enum7.ʏ, true);

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Battery: " + (G_VAR35_float == -1 ? "Ignore" : "Charge up")))
            G_VAR35_float = (G_VAR35_float == -1 ? 1 : -1);

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Hydrogen: " + (G_VAR37_float == -1 ? "Ignore" : "Fill up")))
            G_VAR37_float = (G_VAR37_float == -1 ? 1 : -1);

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " On damage: " + Ϛ(G_VAR21_Enum5))) {
            G_VAR21_Enum5 = ͼ(G_VAR21_Enum5);
        }

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Acceleration: " + Math.Round(G_VAR38_float * 100f) + "%" + (G_VAR38_float > 0.80f ? " (risky)" : ""))) {
            ʈ(ref G_VAR38_float, 0.1f, Enum7.ª, false);
        }
    } else if (G_VAR7 == Enum1.ϵ) {
        double Ʌ = 0;
        if (G_VAR51_List_Class1.Count > 0)
            Ʌ = Vector3.Distance(G_VAR51_List_Class1.Last().ɉ, ɉ);

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Stop path recording"))
            ˎ();

        if (ƽ != Enum13.Ͼ) {
            if (ύ(ref Ɨ, ƴ, A++, ƛ, " Home: " + (G_VAR46_bool ? "Use old home" : (G_VAR43_Class1.Ͷ ? "Was set! " : "none "))))
                G_VAR46_bool = !G_VAR46_bool;
        } else {
            if (ύ(ref Ɨ, ƴ, A++, ƛ, " Connector 1: " + (G_VAR46_bool ? "Use old connector" : (G_VAR43_Class1.Ͷ ? "Was set! " : "none "))))
                G_VAR46_bool = !G_VAR46_bool;

            if (ύ(ref Ɨ, ƴ, A++, ƛ, " Connector 2: " + (G_VAR47_bool ? "Use old connector" : (G_VAR58_Class1.Ͷ ? "Was set! " : "none "))))
                G_VAR47_bool = !G_VAR47_bool;
        }

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Path: " + (G_VAR48_bool ? "Use old path" : (G_VAR51_List_Class1.Count > 1 ? "Count: " + G_VAR51_List_Class1.Count : "none "))))
            G_VAR48_bool = !G_VAR48_bool;

        Ɨ += ƶ;
        Ɨ += "Wp spacing: " + Math.Round(G_VAR54_double) + "m\n";
    } else if (G_VAR7 == Enum1.ϲ) {
        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Stop")) {
            Ҕ();
            Љ(Enum1.Ϸ);
        }

        Ɨ += ƶ;
        Ɨ += "State: " + ϕ(G_VAR68_Enum10) + " \n";
        Ɨ += "Speed: " + Math.Round(κ, 1) + "m/s\n";
        Ɨ += "Target dist: " + Ϣ + "m\n";
        Ɨ += "Wp count: " + G_VAR51_List_Class1.Count + "\n";
        Ɨ += "Wp left: " + Ԋ + "\n";
    } else if (G_VAR7 == Enum1.ϯ) {
        List<IMyTerminalBlock> ʢ = į();
        if (ο)
            G_VAR9++;

        if (G_VAR9 >= ʢ.Count)
            G_VAR9 = 0;

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Next"))
            Љ(Enum1.Ϯ);

        Ɨ += ƶ;
        Ɨ += "Version: " + VERSION + "\n";
        Ɨ += "Ship load: " + Math.Round(ŏ, 1) + "% " + Math.Round(œ, 1) + " / " + Math.Round(ł, 1) + "\n";
        Ɨ += "Uranium: " + (Ц.Count == 0 ? "No reactors" : Math.Round(Ĺ, 1) + "Kg " + ĸ) + "\n";
        Ɨ += "Battery: " + (ĥ == Enum12.ɗ ? "" : Ĩ + "% ") + ϗ(ĥ) + "\n";
        Ɨ += "Hydrogen: " + (Ф.Count == 0 ? "No tanks" : Math.Round(ļ, 1) + "% ") + "\n";
        Ɨ += "Damage: " + (ʢ.Count == 0 ? "None" : "" + (G_VAR9 + 1) + "/" + ʢ.Count + " " + ʢ[G_VAR9].CustomName) + "\n";
    } else if (G_VAR7 == Enum1.Ϯ) {
        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Back"))
            Љ(Enum1.Ϸ);

        Ɨ += ƶ;
        Ɨ += "Next scan: " + Ϊ + "s\n";
        Ɨ += "Ship size: " + Math.Round(Й, 1) + "m " + Math.Round(Я, 1) + "m " + Math.Round(Ю, 1) + "m \n";
        Ɨ += "Broadcast: " + (ɯ ? "Online - " + ɮ : "Offline") + "\n";
        Ɨ += "Max Instructions: " + Math.Round(Ψ * 100f, 1) + "% \n";
    } else if (G_VAR7 == Enum1.ϭ) {
        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Back"))
            Љ(Enum1.Ϸ);

        Ɨ += ƶ;
        Ɨ += "1. Dock to your docking station\n";
        Ɨ += "2. Select Record path & set home\n";
        Ɨ += "3. Fly the path to the ores\n";
        Ɨ += "4. Select stop path recording\n";
        Ɨ += "5. Align ship in mining direction\n";
        Ɨ += "6. Select Setup job and start\n";
    }

    if (ɘ == 2)
        Ɨ = "Fatal setup error\nNext scan: " + Ϊ + "s\n";

    if (ɜ)
        Ɨ = "Recompile script now";

    int ã = Ɨ.Split('\n').Length;

    for (int ʡ =ã; ʡ <= 10; ʡ++)
        Ɨ += "\n";

    Ɨ += Ʃ;
    Ɨ += "Last: " + G_VAR12 + "\n";
    return Ɨ;
}

void ʠ(bool ʟ, float ʗ) {
    Ҕ();
    Қ(true, false);
    if (ʟ)
        ʈ(ref G_VAR41_float, ʗ, Enum7.ʍ, false);
    else
        ʈ(ref G_VAR42_float, ʗ, Enum7.ʍ, false);

    Ѳ();
    Ş(true, true, 0, 0);
}

float ʞ(float ʝ, float[] ʜ) {
    float J = ʜ[0];
    for (int ʛ = ʜ.Length - 1; ʛ >= 0; ʛ--)
        if (ʝ < ʜ[ʛ])
            J = ʜ[ʛ];

    return J;
}

String ʙ(ref int E) {
    String O = "";
    if (E >= Ы.Count)
        E = -1;

    if (E >= 0) {
        O = Ы[E].CustomName;
    }

    E++;
    return O;
}

void ʘ(string Ô) {
    if (G_VAR61_Enum9 != Enum9.Ӊ)
        return;

    if (Ô == "")
        return;

    IMyTerminalBlock q = GTS.GetBlockWithName(Ô);
    if (q == null || !(q is IMyTimerBlock)) {
        G_VAR12 = "Timerblock " + Ô + " not found!";
        return;
    } 
    
    ((IMyTimerBlock)q).Trigger();
}

void ő(ref int ʗ, int ʖ, int ʚ, int ʕ) {
    if (ʖ == 0)
        return;

    if (ʗ < ʚ && ʕ > 0 || ʗ <= ʚ && ʕ < 0) {
        ʗ += ʕ;
        return;
    }

    int ʤ = Math.Abs(ʖ);
    int ĝ = 0;
    int ʹ = 1;
    while (true) {
        ĝ += ʹ * ʤ * 10;
        if (ʖ < 0 && ʗ - ʚ <= ĝ)
            break;

        if (ʖ > 0 && ʗ - ʚ < ĝ)
            break;
        ʹ++;
    }

    ʗ += ʹ * ʖ;
}

void ʸ(bool ʷ) {
    G_VAR24_int = Math.Max(G_VAR24_int, 1);
    G_VAR25_int = Math.Max(G_VAR25_int, 1);
    G_VAR26_int = Math.Max(G_VAR26_int, 0);
    Ş(G_VAR7 == Enum1.ϴ, false, 0, 0);
}

/**
 * Original: ʶ
 */ 
public enum Enum3 {
    ʵ,
    ʴ,
    ʳ
}

/**
 * Original: ʲ
 */
public enum Enum4 {
    ʱ,
    ʰ
}

/**
 * Original: ʯ
 */
public enum Enum5 {
    ʮ,
    ʭ,
    ʬ,
    ʫ
}

/**
 * Original: ʪ
 */
public enum Enum6 {
    ɗ,
    ʩ,
    ʨ,
    ʧ,
    ʦ,
    ʥ,
    ʺ
}

/**
 * Original: ʔ
 */
Class5 G_VAR19_Class5 = new Class5();

/**
 * Original: ʒ
 */
Class5 G_VAR20_Class5 = new Class5();

/**
 * Original: ʆ
 */
Enum5 G_VAR21_Enum5 = Enum5.ʮ;

/**
 * Original: ʅ
 */
bool G_VAR22_bool = true;

/**
 * Original: ʄ
 */
Enum4 G_VAR23_Enum4 = Enum4.ʱ;

/**
 * Original: ʃ
 */
int G_VAR24_int = 3;

/**
 * Original: ʂ
 */
int G_VAR25_int = 3;

/**
 * Original: ʁ
 */
int G_VAR26_int = 30;

/**
 * Original: ʀ
 */
Enum3 G_VAR27_Enum3 = Enum3.ʵ;

/**
 * Original: ɿ
 */
bool G_VAR28_bool = true;

/**
 * Original: ɾ
 */
bool G_VAR29_bool = true;

/**
 * Original: ʇ
 */
bool G_VAR30_bool = true;

/**
 * Original: ɽ
 */
bool G_VAR31_bool = false;

/**
 * Original: ɻ
 */
Enum6 G_VAR32_Enum6 = Enum6.ɗ;

/**
 * Original: ɺ
 */
bool G_VAR33_bool = true;

/**
 * Original: ɹ
 */
float G_VAR34_float = 90;

/**
 * Original: ɸ
 */
float G_VAR35_float = 20;

/**
 * Original: ɷ
 */
float G_VAR36_float = 5;

/**
 * Original: ɶ
 */
float G_VAR37_float = 20;

/**
 * Original: ɵ
 */
float G_VAR38_float = 0.70f;

/**
 * Original: ɴ 
 * Looks like down mining speed.
 */
float G_VAR39_float = 1.50f;

/**
 * Original: ɳ
 * Looks like up mining speed.
 */
float G_VAR40_float = 2.50f;

/**
 * Original: ɼ
 */
float G_VAR41_float = 0f;

/**
 * Original: ɲ
 */
float G_VAR42_float = 0f;

/**
 * Original: ʉ
 */
public enum Enum7 {
    ʓ,
    ª,
    Ă,
    ʑ,
    ʐ,
    ʏ,
    ʎ,
    K,
    ʍ
};

bool ʈ(ref float A, bool ʌ, Enum7 ž, String O, String ʊ) {
    if (O == "")
        return false;

    float J = -1;
    bool ʋ = false;
    if (O.ToUpper() == ʊ)
        ʋ = true;
    else if (!float.TryParse(O, out J))
        return false;
    else
        J = Math.Max(0, J);

    if (ʌ)
        J = (float)Math.Round(J);

    ʈ(ref A, J, ž, ʋ, false);
    return true;
}

void ʈ(ref float A, float ő, Enum7 ž, bool ʊ) {
    ʈ(ref A, A + ő, ž, ʊ, true);
}

void ʈ(ref float A, float ʻ, Enum7 ž, bool ʊ, bool Έ) {
    float ĝ = 0;
    float ȵ = 0;
    if (ž == Enum7.ʓ) {
        ȵ = 0.5f;
        ĝ = 10f;
    }

    if (ž == Enum7.ª) {
        ȵ = 0.1f;
        ĝ = 1f;
    }

    if (ž == Enum7.ʑ) {
        ȵ = 50f;
        ĝ = 100f;
    }

    if (ž == Enum7.ʐ) {
        ȵ = 5f;
        ĝ = 30f;
    }

    if (ž == Enum7.ʏ) {
        ȵ = 1f;
        ĝ = 25f;
    }

    if (ž == Enum7.Ă) {
        ȵ = 10f;
        ĝ = 95f;
    }

    if (ž == Enum7.ʎ) {
        ȵ = 10f;
        ĝ = 90f;
    }

    if (ž == Enum7.K) {
        ȵ = 10f;
        ĝ = 1800;
    }

    if (ž == Enum7.ʍ) {
        ȵ = 0.0f;
        ĝ = 0.75f;
    }

    if (ʻ == -1 && ʊ) {
        A = -1;
        return;
    }

    if (A == -1)
        ʊ = false;

    bool Ç = ʻ < ȵ || ʻ > ĝ;

    if (Ç && Έ) {
        if (ʻ < A)
            A = ĝ;
        else if (ʻ > A)
            A = ȵ;
    } else
        A = ʻ;

    if (Ç && ʊ)
        A = -1;
    else
        A = Math.Max(ȵ, Math.Min(A, ĝ));

    A = (float)Math.Round(A, 2);
}

void Ά(bool ͽ) {
    if (ͽ)
        ԑ = Math.Max(0, ԑ - 1);
    else
        ԑ++;

    З(true);
}

ŕ ͼ<ŕ>(ŕ ͺ) {
    int ª = Array.IndexOf(Enum.GetValues(ͺ.GetType()), ͺ);
    ª++;
    if (ª >= ͻ(ͺ))
        ª = 0;
    return (ŕ)Enum.GetValues(ͺ.GetType()).GetValue(ª);
}

int ͻ<ŕ>(ŕ ͺ) {
    return Enum.GetValues(ͺ.GetType()).Length;
}

/**
 * Original: ͷ 
 */
class Class1 {
    public bool Class1 = false;
    public Vector3 ɉ = new Vector3();
    public Vector3 ç = new Vector3();
    public Vector3 Ï = new Vector3();
    public Vector3 ĕ = new Vector3();
    public Vector3 Í = new Vector3();
    public Vector3 Ή = new Vector3();
    public float Ί = 0;
    public float Η = 0;
    public float[] Θ = null;
    public Class1() {

    }
    public Class1(Class1 Ζ) {
        Ͷ = Ζ.Ͷ;
        ɉ = Ζ.ɉ;
        ç = Ζ.ç;
        Ï = Ζ.Ï;
        ĕ = Ζ.ĕ;
        Í = Ζ.Í;
        Ή = Ζ.Ή;
        Θ = Ζ.Θ;
    }

    public Class1(Vector3 ɉ, Vector3 Ï, Vector3 ç, Vector3 ĕ, Vector3 Í) {
        this.ɉ = ɉ;
        this.ç = ç;
        this.Ï = Ï;
        this.ĕ = ĕ;
        this.Ί = 0;
        this.Í = Í;
    }

    public void Ε(List<IMyThrust> Δ, List<string> Γ) {
        Θ = new float[Γ.Count];
        for (int A = 0; A < Θ.Length; A++)
            Θ[A] = -1;

        for (int A = 0; A < Δ.Count; A++) {
            string O = L(Δ[A]);
            int E = Γ.IndexOf(O);
            if (E != -1)
                Θ[E] = µ(Δ[A].MaxEffectiveThrust, Δ[A].MaxThrust);
        }
    }
}

/**
 * Original: Β
 */
Class1 G_VAR43_Class1 = new Class1();

/**
 * Original: Α
 */
Class1 G_VAR44_Class1 = new Class1();

/**
 * Original: ΐ
 */
Class1 G_VAR45_Class1 = new Class1();

/**
 * Original: Ώ
 */
bool G_VAR46_bool = false;

/**
 * Original: Ύ 
 */
bool G_VAR47_bool = false;

/**
 * Original: Ό
 */
bool G_VAR48_bool = false;

void ʹ() {
    G_VAR52_List_Class1.Clear();

    for (int A = 0; A < G_VAR51_List_Class1.Count; A++)
        G_VAR52_List_Class1.Add(G_VAR51_List_Class1[A]);

    G_VAR51_List_Class1.Clear();
    G_VAR49_bool = true;
    G_VAR44_Class1 = new Class1(G_VAR43_Class1);
    G_VAR45_Class1 = new Class1(G_VAR58_Class1);
    G_VAR43_Class1.Ͷ = false;
    if (ƽ == Enum13.Ͼ)
        G_VAR58_Class1.Ͷ = false;

    for (int A = 0; A < ä.Count; A++)
        if (!G_VAR50_List_String.Contains(ä.Keys.ElementAt(A)))
            G_VAR50_List_String.Add(ä.Keys.ElementAt(A));

    G_VAR46_bool = false;
    G_VAR47_bool = false;
    G_VAR48_bool = false;
    Љ(Enum1.ϵ);
}

void ˎ() {
    if (G_VAR46_bool) G_VAR43_Class1 = G_VAR44_Class1;
    if (G_VAR47_bool) G_VAR58_Class1 = G_VAR45_Class1;
    if (G_VAR48_bool) {
        G_VAR51_List_Class1.Clear();
        for (int A = 0; A < G_VAR52_List_Class1.Count; A++)
            G_VAR51_List_Class1.Add(G_VAR52_List_Class1[A]);
    }

    G_VAR49_bool = false;
    Ҕ();
    Љ(Enum1.Ϸ);
}

/**
 * Original: ˍ
 */
bool G_VAR49_bool = false;

/**
 * Original: ˌ
 */
List<String> G_VAR50_List_String = new List<string>();

/**
 * Original: ˋ
 */
List<Class1> G_VAR51_List_Class1 = new List<Class1>();

/**
 * Original: ˊ
 */
List<Class1> G_VAR52_List_Class1 = new List<Class1>();

/**
 * Original: ˉ
 */
int G_VAR53_int = 0;

/**
 * Original:ˈ
 */
double G_VAR54_double = 0;

void ˇ() {
    if (!G_VAR49_bool)
        return;

    if (G_VAR68_Enum10 != Enum10.Д) {
        ˎ();
        return;
    }

    if (!G_VAR44_Class1.Ͷ)
        G_VAR46_bool = false;

    if (!G_VAR45_Class1.Ͷ)
        G_VAR47_bool = false;

    if (G_VAR52_List_Class1.Count <= 1)
        G_VAR48_bool = false;

    IMyShipConnector º = Ò(MyShipConnectorStatus.Connectable);
    if (º == null)
        º = Ò(MyShipConnectorStatus.Connected);

    if (º != null) {
        if (Math.Round(κ, 2) <= 0.20)
            G_VAR53_int++;
        else
            G_VAR53_int = 0;
        if (G_VAR53_int >= 5) {
            if (ƽ == Enum13.Ͼ && (G_VAR43_Class1.Ͷ || G_VAR46_bool) && Vector3.Distance(G_VAR43_Class1.ɉ, º.GetPosition()) > 5) {
                G_VAR58_Class1.Ï = Э.WorldMatrix.Forward;
                G_VAR58_Class1.ĕ = Э.WorldMatrix.Left;
                G_VAR58_Class1.ç = Э.WorldMatrix.Down;
                G_VAR58_Class1.Í = Э.GetNaturalGravity();
                G_VAR58_Class1.ɉ = º.GetPosition();
                G_VAR58_Class1.Ͷ = true;
                G_VAR58_Class1.Ή = º.Position;
            } else {
                G_VAR43_Class1.Ï = Э.WorldMatrix.Forward;
                G_VAR43_Class1.ĕ = Э.WorldMatrix.Left;
                G_VAR43_Class1.ç = Э.WorldMatrix.Down;
                G_VAR43_Class1.Í = Э.GetNaturalGravity();
                G_VAR43_Class1.ɉ = º.GetPosition();
                G_VAR43_Class1.Ͷ = true;
                G_VAR43_Class1.Ή = º.Position;
            }
        }
    }

    double ˆ = -1;
    if (G_VAR51_List_Class1.Count > 0) {
        ˆ = Vector3.Distance(ɉ, G_VAR51_List_Class1.Last().ɉ);
    }

    double ğ = Math.Max(1.5, Math.Pow(κ / 100.0, 2));
    double ˁ = Math.Max(κ * ğ, 2);
    G_VAR54_double = ˁ;
    if ((ˆ == -1) || ˆ >= ˁ) {
        Class1 B = new Class1(ɉ, υ, σ, τ, Э.GetNaturalGravity());
        B.Ε(Δ, G_VAR50_List_String);
        G_VAR51_List_Class1.Add(B);
    }
}

int ˀ(Vector3 ý, int ʿ) {
    if (ʿ == -1)
        return 0;

    double ʾ = -1;
    int ʽ = -1;
    for (int A = G_VAR51_List_Class1.Count - 1; A >= 0; A--) {
        double Ʌ = Vector3.Distance(G_VAR51_List_Class1[A].ɉ, ý); if (ʾ == -1 || Ʌ < ʾ) {
            ʽ = A;
            ʾ = Ʌ;
        }
    }

    return Math.Sign(ʽ - ʿ);
}

bool ʼ(Vector3 ɉ) {
    List<Vector3> J = new List<Vector3>();
    for (int A = 0; A < G_VAR51_List_Class1.Count; A++) {
        J.Add(G_VAR51_List_Class1[A].ɉ);
    }

    if (G_VAR43_Class1.Ͷ && G_VAR51_List_Class1.Count >= 1) {
        Vector3 ͳ = new Vector3();
        ˠ(G_VAR43_Class1, dockDist * Ю, false, out ͳ);
        if (Vector3.Distance(G_VAR43_Class1.ɉ, G_VAR51_List_Class1.First().ɉ) < Vector3.Distance(G_VAR43_Class1.ɉ, G_VAR51_List_Class1.Last().ɉ)) {
            J.Insert(0, ͳ);
            J.Insert(0, G_VAR43_Class1.ɉ);
        } else {
            J.Add(ͳ);
            J.Add(G_VAR43_Class1.ɉ);
        }
    }

    if (ƽ == Enum13.Ͼ) {
        if (G_VAR58_Class1.Ͷ && G_VAR51_List_Class1.Count >= 1) {
            Vector3 Ͳ = new Vector3();
            ˠ(G_VAR58_Class1, dockDist * Ю, false, out Ͳ);
            if (Vector3.Distance(G_VAR58_Class1.ɉ, G_VAR51_List_Class1.First().ɉ) < Vector3.Distance(G_VAR58_Class1.ɉ, G_VAR51_List_Class1.Last().ɉ)) {
                J.Insert(0, Ͳ); J.Insert(0, G_VAR58_Class1.ɉ);
            }
            else {
                J.Add(Ͳ);
                J.Add(G_VAR58_Class1.ɉ);
            }
        }
    } else {
        if (G_VAR61_Enum9 != Enum9.Ӌ)
            if (G_VAR51_List_Class1.Count > 0 && Vector3.Distance(G_VAR58_Class1.ɉ, G_VAR51_List_Class1.First().ɉ) < Vector3.Distance(G_VAR58_Class1.ɉ, G_VAR51_List_Class1.Last().ɉ))
                J.Insert(0, G_VAR58_Class1.ɉ);
            else
                J.Add(G_VAR58_Class1.ɉ);
    }

    int ʽ = -1;
    double ͱ = -1;
    for (int A = 0; A < J.Count; A++) {
        double Ʌ = Vector3.Distance(J[A], ɉ);
        if (Ʌ < ͱ || ͱ == -1) {
            ͱ = Ʌ;
            ʽ = A;
        }
    }

    if (J.Count == 0)
        return false;

    double Ͱ = Vector3.Distance(J[ʽ], ɉ);
    double ˮ = Vector3.Distance(J[Math.Max(0, ʽ - 1)], J[ʽ]) * 1.5f;
    double ˬ = Vector3.Distance(J[Math.Min(J.Count - 1, ʽ + 1)], J[ʽ]) * 1.5f;
    return Ͱ < ˮ || Ͱ < ˬ;
}

/**
 * Original: ˤ
 */
Class1 G_VAR55_Class1 = null;

void ˣ(Class1 B, Enum9 ˢ) {
    G_VAR55_Class1 = B;
    if (G_VAR61_Enum9 == Enum9.Ӊ)
        G_VAR69_Enum9 = ˢ;
}

Class1 ˡ() {
    if (ƽ != Enum13.Ͼ)
        return G_VAR43_Class1;

    return G_VAR55_Class1;
}

bool ˠ(Class1 ˑ, float Ʌ, bool ː, out Vector3 ˏ) {
    if (ː) {
        Vector3I Ƌ = new Vector3I((int)ˑ.Ή.X, (int)ˑ.Ή.Y, (int)ˑ.Ή.Z);
        IMySlimBlock ʣ = Me.CubeGrid.GetCubeBlock(Ƌ);
        if (ʣ == null || !(ʣ.FatBlock is IMyShipConnector)) {
            ˏ = new Vector3();
            return false;
        }

        Vector3 đ = Ȑ(Э, ʣ.FatBlock.GetPosition() - ɉ);
        Vector3 м = Ȑ(Э, ʣ.FatBlock.WorldMatrix.Forward);
        ˏ = ˑ.ɉ - Ȏ(ˑ.Ï, ˑ.ç * -1, đ) - Ȏ(ˑ.Ï, ˑ.ç * -1, м) * Ʌ;
        return true;
    } else {
        ˏ = ˑ.ɉ;
        return true;
    }
}

/**
 * Original: ҿ
 */
Vector3 G_VAR56_Vector3 = new Vector3();

/**
 * Original: ҽ
 */
bool G_VAR57_bool = false;
Vector3 Ҽ(int Y, int X, bool һ) {
    if (!һ && G_VAR57_bool)
        return G_VAR56_Vector3;

    float Ă = ((G_VAR63_int - 1f) / 2f) - Y;
    float K = ((G_VAR64_int - 1f) / 2f) - X; G_VAR56_Vector3 = G_VAR58_Class1.ɉ + G_VAR58_Class1.ĕ * Ă * Й + G_VAR60_Vector3 * -1 * K * Я;
    G_VAR57_bool = true;
    return G_VAR56_Vector3;
}

Vector3 Һ(Vector3 ҹ, float Ҿ) {
    return ҹ + (G_VAR59_Vector3 * Ҿ);
}

/**
 * Original: Ҹ
 */
public enum Enum8 {
    ҷ,
    Ҷ,
    ґ,
    Ҍ,
    ҵ,
    Ҵ
}

Enum8 ҳ() {
    float Ʌ = -1;
    Enum8 ª = Enum8.ҷ;
    if (ƽ != Enum13.Ͼ) {
        if (G_VAR61_Enum9 != Enum9.Ӌ) {
            Vector3 ț = ȕ(G_VAR59_Vector3, G_VAR60_Vector3 * -1, ɉ - G_VAR58_Class1.ɉ);
            if (Math.Abs(ț.X) <= (float)(G_VAR63_int * Й) / 2f && Math.Abs(ț.Y) <= (float)(G_VAR64_int * Я) / 2f) {
                if (ț.Z <= -1 && ț.Z >= -Ҍ * 2)
                    return Enum8.Ҍ;

                if (ț.Z > -1 && ț.Z < Ю * 2)
                    return Enum8.ҷ;
            }

            if (Ӎ(G_VAR58_Class1.ɉ, ref Ʌ))
                ª = Enum8.ҷ;
        }

        if (G_VAR43_Class1.Ͷ) {
            if (Ӎ(G_VAR43_Class1.ɉ, ref Ʌ))
                ª = Enum8.ґ;

            for (int A = 0; A < G_VAR51_List_Class1.Count; A++) {
                if (Ӎ(G_VAR51_List_Class1[A].ɉ, ref Ʌ))
                    ª = Enum8.Ҷ;
            }

            if (Vector3.Distance(ɉ, G_VAR43_Class1.ɉ) < dockDist * Ю)
                ª = Enum8.ґ;

            if (Ò(MyShipConnectorStatus.Connectable) != null || Ò(MyShipConnectorStatus.Connected) != null)
                ª = Enum8.ґ;
        }
    } else {
        Vector3 ɉ = new Vector3();
        IMyShipConnector Ä = Ò(MyShipConnectorStatus.Connected);
        if (G_VAR43_Class1.Ͷ) {
            if (Ӎ(G_VAR43_Class1.ɉ, ref Ʌ))
                ª = Enum8.ґ;

            if (ˠ(G_VAR43_Class1, dockDist, true, out ɉ))
                if (Ӎ(ɉ, ref Ʌ))
                    ª = Enum8.ґ;

            if (Ä != null && Vector3.Distance(Ä.GetPosition(), G_VAR43_Class1.ɉ) < 5)
                return Enum8.ҵ;
        }

        for (int A = 0; A < G_VAR51_List_Class1.Count; A++)
            if (Vector3.Distance(G_VAR51_List_Class1[A].ɉ, G_VAR43_Class1.ɉ) > dockDist * Ю && Vector3.Distance(G_VAR51_List_Class1[A].ɉ, G_VAR58_Class1.ɉ) > dockDist * Ю)
                if (Ӎ(G_VAR51_List_Class1[A].ɉ, ref Ʌ))
                    ª = Enum8.Ҷ;

        if (G_VAR58_Class1.Ͷ) {
            if (Ӎ(G_VAR58_Class1.ɉ, ref Ʌ))
                ª = Enum8.ҷ;

            if (ˠ(G_VAR58_Class1, dockDist, true, out ɉ))
                if (Ӎ(ɉ, ref Ʌ))
                    ª = Enum8.ҷ;

            if (Ä != null && Vector3.Distance(Ä.GetPosition(), G_VAR58_Class1.ɉ) < 5)
                return Enum8.Ҵ;
        }
    }

    return ª;
}

bool Ӎ(Vector3 ø, ref float Ʌ) {
    float ț = Vector3.Distance(ø, ɉ);
    if (ț < Ʌ || Ʌ == -1) {
        Ʌ = ț;
        return true;
    }

    return false;
}

/**
 * Original: ӌ
 */
public enum Enum9 {
    Ӌ,
    ӊ,
    Ӊ,
    ΰ,
    Ғ,
    ғ,
    ӈ,
    Ӈ,
    ӆ
}

/**
 * Original: Ǟ
 */
Class1 G_VAR58_Class1 = new Class1();

/**
 * Original: Ӆ
 */
Vector3 G_VAR59_Vector3;

/**
 * Original: ӄ
 */
Vector3 G_VAR60_Vector3;

/**
 * Original: Ӄ
 */
Enum9 G_VAR61_Enum9 = Enum9.Ӌ;

/**
 * Original: ӂ
 */
Enum4 G_VAR62_Enum4 = Enum4.ʱ;

/**
 * Original: Ӂ
 */
int G_VAR63_int = 0;

/**
 * Original: Ӏ
 */
int G_VAR64_int = 0;

/**
 * Original: ӎ
 */
double G_VAR65_double = 0;

/**
 * Original: Ҳ
 */
bool G_VAR66_bool = false;

void Ұ() {
    if (ɘ > 0) {
        G_VAR12 = "Setup error! Can't start";
        return;
    }

    if (ƽ == Enum13.Ͼ) {
        қ();
        return;
    }

    G_VAR58_Class1.ɉ = ɉ;
    G_VAR58_Class1.Í = Э.GetNaturalGravity();
    G_VAR58_Class1.Ï = υ;
    G_VAR58_Class1.ç = σ;
    G_VAR58_Class1.ĕ = τ;
    G_VAR59_Vector3 = М.WorldMatrix.Forward;
    G_VAR60_Vector3 = G_VAR58_Class1.ç;
    if (G_VAR59_Vector3 == Э.WorldMatrix.Down)
        G_VAR60_Vector3 = Э.WorldMatrix.Backward;

    Қ(true, true);
    Ҝ(Enum10.ʟ);
    ҕ();
}

void Қ(bool Ç, bool ҙ) {
    if (G_VAR61_Enum9 == Enum9.Ӌ && !Ç)
        return;

    bool Ҙ = Ç || G_VAR61_Enum9 == Enum9.ΰ || G_VAR63_int != G_VAR24_int || G_VAR64_int != G_VAR25_int || G_VAR62_Enum4 != G_VAR23_Enum4;

    if (Ҙ) {
        if (G_VAR61_Enum9 != Enum9.Ӌ) {
            G_VAR61_Enum9 = Enum9.Ғ;
            Ҽ(ԓ, Ԓ, ҙ);
            G_VAR12 = "Job changed, lost progress";
        }

        G_VAR62_Enum4 = G_VAR23_Enum4;
        G_VAR63_int = G_VAR24_int;
        G_VAR64_int = G_VAR25_int;
        Ԓ = 0;
        ԓ = 0;
        Ԁ = 0;
        Ԑ = 0;
        ԁ = 0;
        ԑ = 0;
        З(true);
    }
}

void җ() {
    ñ(ɉ, 0);
    Ŗ(Δ, true);
}

/**
 * Original: Җ
 */
int G_VAR67_int = 0;

void ҕ() {
    Ͻ(Enum2.Ђ);
    җ();
    ť(Ч, false);
    Ũ(ũ);
    G_VAR61_Enum9 = Enum9.Ӊ;
    È(true);
    G_VAR69_Enum9 = G_VAR61_Enum9;
    Љ(Enum1.ϳ);
    К();
    G_VAR66_bool = true;
    G_VAR67_int = 0;
    for (int A = У.Count - 1; A >= 0; A--)
        if (ƃ(У[A], false))
            G_VAR67_int++;

    if (G_VAR67_int > 0)
        G_VAR12 = "Started with damage";
}

void Ҕ() {
    if (G_VAR61_Enum9 == Enum9.Ӊ) {
        G_VAR61_Enum9 = Enum9.ӊ;
        G_VAR12 = "Job paused";
    }

    Ҝ(Enum10.Д);
    G_VAR69_Enum9 = G_VAR61_Enum9;
    á(false, 0, 0, 0, 0);
    å();
    k(new Vector3(), false);
    ì();
    ŧ(ChargeMode.Auto);
    Ŕ(false);
    ű(true);
    Ӿ(Enum10.Д);
    Ş(false, false, 0, 0);
    Ŗ(Ш, false);
    Ŗ(Ъ, true);
    Ũ(true);
    ԋ = false;
    G_VAR66_bool = false;
    Ɔ = false;
    G_VAR2 = false;
    if (G_VAR7 != Enum1.Ϸ && G_VAR7 != Enum1.ϱ && G_VAR7 != Enum1.ϰ && G_VAR7 != Enum1.Џ)
        Љ(Enum1.Ϸ);
}

void қ() {
    Enum8 Ҏ = ҳ();
    if (ƽ == Enum13.Ͼ) {
        if (!G_VAR58_Class1.Ͷ || !G_VAR43_Class1.Ͷ)
            return;

        ҕ();
        bool ғ = Vector3.Distance(ɉ, G_VAR43_Class1.ɉ) < Vector3.Distance(ɉ, G_VAR58_Class1.ɉ);

        if (ɛ == Enum9.Ӈ)
            ғ = true;

        if (ɛ == Enum9.ӆ)
            ғ = false;

        if (ғ) {
            ˣ(G_VAR43_Class1, Enum9.Ӈ);
            switch (Ҏ) {
                case Enum8.ҵ:
                    Ҝ(Enum10.ҡ);
                    break;

                case Enum8.Ҷ:
                    Ҝ(Enum10.Ү);
                    break;

                case Enum8.ґ:
                    Ҝ(Enum10.Ҭ);
                    break;

                default:
                    Ҝ(Enum10.ҧ);
                    break;
            }
        } else {
            ˣ(G_VAR58_Class1, Enum9.ӆ);
            switch (Ҏ) {
                case Enum8.Ҵ:
                    Ҝ(Enum10.ҡ);
                    break;

                case Enum8.ҷ:
                    Ҝ(Enum10.Ҭ);
                    break;

                case Enum8.Ҷ:
                    Ҝ(Enum10.Ү);
                    break;

                default:
                    Ҝ(Enum10.ҧ);
                    break;
            }
        }
    } else {
        if (G_VAR61_Enum9 != Enum9.ӊ && G_VAR61_Enum9 != Enum9.Ғ)
            return;

        bool Ғ = G_VAR61_Enum9 == Enum9.Ғ;
        ҕ();
        bool ґ = Ű(false) && G_VAR43_Class1.Ͷ;
        switch (Ҏ) {
            case Enum8.ҷ:
                Ҝ(ґ ? Enum10.Ҧ : Enum10.ʟ);
                break;

            case Enum8.Ҷ:
                Ҝ(ґ ? Enum10.ү : Enum10.Ү);
                break;

            case Enum8.ґ:
                Ҝ(ґ ? Enum10.Ҭ : Enum10.Ҩ);
                break;

            case Enum8.Ҍ:
                {
                    if (Ԑ != ԑ || Ғ)
                        Ҝ(Enum10.ҝ);
                    else
                        Ҝ(Enum10.Ҍ);
                }
                break;

            default:
                break;
        }
    }
}

void Ґ() {
    if (G_VAR61_Enum9 == Enum9.Ӌ && !G_VAR43_Class1.Ͷ)
        return;

    if (ƽ == Enum13.Ͼ && (!G_VAR58_Class1.Ͷ || !G_VAR43_Class1.Ͷ))
        return;

    G_VAR12 = "Move to job";
    Enum8 Ҏ = ҳ();

    if (ƽ == Enum13.Ͼ) {
        ˣ(G_VAR58_Class1, Enum9.ӆ);
        switch (Ҏ) {
            case Enum8.ҷ:
                Ҝ(Enum10.Ҭ);
                break;

            case Enum8.Ҷ:
                Ҝ(Enum10.Ү);
                break;

            case Enum8.Ҵ:
                return;

            default:
                Ҝ(Enum10.ҧ);
                break;
        }

        Ӿ(Enum10.ҡ);
    } else {
        switch (Ҏ) {
            case Enum8.ҷ:
                Ҝ(Enum10.Ү);
                break;

            case Enum8.Ҷ:
                Ҝ(Enum10.Ү);
                break;

            case Enum8.ґ:
                Ҝ(Enum10.Ҩ);
                break;

            case Enum8.Ҍ:
                Ҝ(Enum10.ҝ);
                break;

            default:
                break;
        }

        if (G_VAR61_Enum9 == Enum9.Ӌ)
            Ӿ(Enum10.Ү);
        else
            Ӿ(Enum10.Ҫ);

        ԋ = true;
    }

    җ();
    Љ(Enum1.ϲ);
    ť(Ч, false);
    G_VAR69_Enum9 = Enum9.ӈ;
}

void ҏ() {
    if (!G_VAR43_Class1.Ͷ)
        return;

    G_VAR12 = "Move home";
    Enum8 Ҏ = ҳ();

    if (ƽ == Enum13.Ͼ) {
        ˣ(G_VAR43_Class1, Enum9.Ӈ);
        switch (Ҏ) {
            case Enum8.Ҷ:
                Ҝ(Enum10.ү);
                break;

            case Enum8.ґ:
                Ҝ(Enum10.Ҭ);
                break;

            case Enum8.ҵ:
                return;

            default:
                Ҝ(Enum10.ҧ);
                break;
        }

        Ӿ(Enum10.ҡ);
    } else {
        if (Ò(MyShipConnectorStatus.Connected) != null)
            return;

        if (Ò(MyShipConnectorStatus.Connectable) != null) {
            Ҝ(Enum10.ґ);
            Ӿ(Enum10.Ҩ);
            return;
        }

        switch (Ҏ) {
            case Enum8.ҷ:
                Ҝ(Enum10.Ҧ);
                break;

            case Enum8.Ҷ:
                Ҝ(Enum10.ү);
                break;

            case Enum8.ґ:
                Ҝ(Enum10.ү);
                break;

            case Enum8.Ҍ:
                Ҝ(Enum10.ұ);
                break;

            default:
                break;
        }

        Ӿ(Enum10.Ҩ);
    }

    җ();
    Љ(Enum1.ϲ);
    ť(Ч, false);
    G_VAR69_Enum9 = Enum9.ғ;
}

/**
 * Original: ҍ
 */
public enum Enum10 {
    Д, ʟ, Ҍ, ҝ, ұ, ү, Ү, ҭ, Ҭ, ґ, ҫ, Ҫ, ҩ, Ҩ, ҧ, Ҧ, Ŧ, ļ, Ĺ, ҥ, Ҥ, ң, Ң, ҡ,
}

/**
 * Original: Ҡ
 */
Enum10 G_VAR68_Enum10;

/**
 * Original: ҟ
 */
Enum9 G_VAR69_Enum9;

void Ҝ(Enum10 Ҟ) {
    if (Ҟ == Enum10.Д)
        G_VAR70_Enum10 = Enum10.Д;

    if (G_VAR70_Enum10 != Enum10.Д && G_VAR68_Enum10 == G_VAR70_Enum10 && Ҟ != G_VAR70_Enum10) {
        Ҕ();
        return;
    }

    Ԏ = true;
    G_VAR68_Enum10 = Ҟ;
}

/**
 * Original: ӏ
 */
Enum10 G_VAR70_Enum10;

void Ӿ(Enum10 G_VAR70_Enum10) {
    this.G_VAR70_Enum10 = G_VAR70_Enum10;
}

/**
 * Original: Ӽ
 */
Class2 G_VAR71_Class2 = null;

/**
 * Original: ӻ
 */
class Class2 {
    public Class1 Ӻ = null;
    public List<Vector3> ӹ = new List<Vector3>();
    public float Ӹ = 0;
    public float ӷ = 0;
    public float Ӷ = 0;
    public float ӵ = 0;
    public Vector3 Ӵ = new Vector3();
}

/**
 * Original: ӳ
 */
public enum Enum11 {
    Ӳ, ӱ, Ӱ
}

/**
 * Original: ӯ
 */
int[] G_VAR72_int_array = null;

Enum11 Ӯ(int ӭ, bool Ç) {
    if (Ç) {
        G_VAR72_int_array = null;
        ԓ = 0;
        Ԓ = 0;
    }

    if (G_VAR23_Enum4 == Enum4.ʱ) {
        int Ӭ = ӭ + 1;
        Ԓ = (int)Math.Floor(µ(ӭ, G_VAR63_int));
        if (Ԓ % 2 == 0)
            ԓ = ӭ - (Ԓ * G_VAR63_int);
        else
            ԓ = G_VAR63_int - 1 - (ӭ - (Ԓ * G_VAR63_int));

        if (Ԓ >= G_VAR64_int)
            return Enum11.ӱ;
        else
            return Enum11.Ӳ;
    } else if (G_VAR23_Enum4 == Enum4.ʰ) {
        if (G_VAR72_int_array == null)
            G_VAR72_int_array = new int[] { 0, -1, 0, 0 };

        int ӫ = (int)Math.Ceiling(G_VAR63_int / 2f);
        int Ӫ = (int)Math.Ceiling(G_VAR64_int / 2f);
        int ӽ = (int)Math.Floor(G_VAR63_int / 2f);
        int ӿ = (int)Math.Floor(G_VAR64_int / 2f);
        int Ԕ = 0;
        while (G_VAR72_int_array[2] < Math.Pow(Math.Max(G_VAR63_int, G_VAR64_int), 2)) {
            if (Ԕ > 200)
                return Enum11.Ӱ;

            Ԕ++;
            G_VAR72_int_array[2]++;
            if (-ӫ < ԓ && ԓ <= ӽ && -Ӫ < Ԓ && Ԓ <= ӿ) {
                if (G_VAR72_int_array[3] == ӭ) {
                    this.ԓ = ԓ - 1 + ӫ;
                    this.Ԓ = Ԓ - 1 + Ӫ;
                    return Enum11.Ӳ;
                }

                G_VAR72_int_array[3]++;
            }

            if (ԓ == Ԓ || (ԓ < 0 && ԓ == -Ԓ) || (ԓ > 0 && ԓ == 1 - Ԓ)) {
                int ԕ = G_VAR72_int_array[0];
                G_VAR72_int_array[0] = -G_VAR72_int_array[1];
                G_VAR72_int_array[1] = ԕ;
            }

            ԓ += G_VAR72_int_array[0];
            Ԓ += G_VAR72_int_array[1];
        }
    }

    return Enum11.ӱ;
}

int ԓ = 0;
int Ԓ = 0;
int ԑ = 0;
int Ԑ = 0;
int Ҍ = 30;
int ԏ = 0;
bool Ԏ = true;
Vector3 ԍ;
double Ԍ = 0;
bool ԋ = false;
int Ԋ = 0;
int ԉ = 0;
int Ԉ = 0;
int ы = 0;
int ԇ = 0;
Vector3 Ԇ = new Vector3();
float ԅ = 0;
float Ԅ = 0;
float ԃ = 0;
float Ԃ = 0;
float ԁ = 0;
float Ԁ = 0;
bool ө = false;
bool ӛ = false;
bool ӧ = false;
bool Ӛ = false;
bool ә = false;
DateTime ż = new DateTime();
Class1 Ә = null;

void ӗ() {
    if (G_VAR68_Enum10 == Enum10.ʟ) {
        if (Ԏ) {
            ԉ = 0;
            if (Ԑ != ԑ) {
                Ԁ = 0;
            }

            Ԑ = ԑ;
        }

        if (ԉ == 0) {
            Enum11 J = Ӯ(ԑ, Ԏ);
            if (J == Enum11.ӱ) {
                G_VAR61_Enum9 = Enum9.ΰ;
                G_VAR12 = "Job done";
                if (G_VAR22_bool && G_VAR43_Class1.Ͷ) {
                    Ҝ(Enum10.Ҧ);
                    Ӿ(Enum10.Ҩ);
                    G_VAR69_Enum9 = Enum9.ғ;
                } else {
                    Ҝ(Enum10.ҭ);
                    Ӿ(Enum10.Ҫ);
                    G_VAR69_Enum9 = Enum9.ӈ;
                }

                return;
            }

            if (J == Enum11.Ӳ) {
                ԉ = 1;
                Ŗ(Ш, true);
                ԍ = Ҽ(ԓ, Ԓ, true);
                ñ(ԍ, 10);
                ë(G_VAR58_Class1.ç, G_VAR58_Class1.Ï, G_VAR58_Class1.ĕ, false);
            }
        } else {
            if (Ԍ < wpReachedDist) {
                Ҝ(Enum10.Ҍ);
                return;
            }
        }
    }

    if (G_VAR68_Enum10 == Enum10.Ҍ) {
        if (Ԏ) {
            Ŗ(Ш, true);
            Ũ(false);
            ԍ = Ҽ(ԓ, Ԓ, false);
            ñ(Һ(ԍ, 0), 0);
            ë(G_VAR58_Class1.ç, G_VAR58_Class1.Ï, G_VAR58_Class1.ĕ, false);
            ԉ = 1;
            ԅ = 0;
            Ԅ = 0;
            Ԃ = 0;
            ԃ = -1;
            Ҍ = G_VAR26_int;
            ө = true;
        }

        if (!Ĳ()) {
            Ҝ(Enum10.ұ);
            return;
        }

        if (Ű(true)) {
            ԇ = Ő("", "ORE", Enum14.ņ);
            if ((G_VAR32_Enum6 == Enum6.ʧ || G_VAR32_Enum6 == Enum6.ʦ || G_VAR32_Enum6 == Enum6.ʥ || G_VAR32_Enum6 == Enum6.ʺ) && ƽ != Enum13.ψ)
                Ҝ(Enum10.ң);
            else if ((G_VAR32_Enum6 == Enum6.ʩ || G_VAR32_Enum6 == Enum6.ʨ) && ƽ != Enum13.ψ)
                Ҝ(Enum10.ҥ);
            else
                Ҝ(Enum10.ұ);

            return;
        }

        ԁ = Vector3.Distance(ɉ, ԍ);
        if (ԁ > Ԁ) {
            Ԁ = ԁ;
            ө = false;
        }

        if (ƽ == Enum13.ψ && Ж() == MyDetectedEntityType.SmallGrid)
            Ԅ += 2;
        else
            Ԅ -= 2;

        Ԅ = Math.Max(100, Math.Min(400, Ԅ));
        if (ԉ > 0 && ԉ < Ԅ) {
            if (ԁ > ԅ) {
                if (Ԅ > 150) ԅ = ԁ;
                else ԅ = (float)Math.Ceiling(ԁ);
                ԉ = 1;
            } else
                ԉ++;
        } else {
            if (ԉ > 0) {
                G_VAR12 = "Ship stuck! Retrying";
                ԅ = ԁ;
                ԉ = 0;
                Ş(false, true, 0, Ю * sensorRange);
            }

            ñ(Һ(ԍ, Math.Max(0, ԅ - Ю)), Е(false));
            if (Ԍ <= wpReachedDist / 2) {
                ԉ = 1;
                ԅ = 0;
            }

            return;
        }

        Ş(false, true, Ю * sensorRange, 0);
        Vector3 Ӗ = ԍ + G_VAR59_Vector3 * ԁ;
        bool ӕ = false;
        if (Vector3.Distance(Ӗ, ɉ) > 0.3f) {
            Vector3 Ӕ = ԍ + G_VAR59_Vector3 * (ԁ + 0.1f);
            ñ(Ӕ, 4);
            ӕ = true;
        } else {
            float κ = Е(true);
            Vector3 ӓ = Һ(ԍ, Math.Max(G_VAR26_int + 1, ԁ + 1));
            ñ(true, false, false, ӓ, ӓ - ԍ, κ, κ);
        }

        bool ΰ = false;
        if (G_VAR27_Enum3 == Enum3.ʳ || G_VAR27_Enum3 == Enum3.ʴ) {
            if (!ӕ) {
                float Ӓ = 0;
                foreach (IMyTerminalBlock q in Ш)
                    Ӓ += ō(q, "", "", G_VAR27_Enum3 == Enum3.ʴ ? new string[] { "STONE" } : null);

                if (Ӓ > ԃ || ԁ < G_VAR26_int || ө) {
                    Ԉ = 0;
                    Ԃ = ԁ;
                    Ҍ = (int)(Math.Max(Ҍ, Ԃ) + Ю / 2);
                } else {
                    ΰ = ԁ - Ԃ > 2 && Ԉ >= 20;
                    Ԉ++;
                }

                ԃ = Ӓ;
            }
        } else
            ΰ = ԁ >= Ҍ;

        if (Ԑ != ԑ) {
            Ҝ(Enum10.ҝ);
            ԁ = 0;
            return;
        }

        if (ΰ) {
            ԑ++;
            Ҝ(Enum10.ҝ);
            ԁ = 0;
            return;
        }
    }

    if (G_VAR68_Enum10 == Enum10.Ң) {
        bool ΰ = false;
        if (Ԏ) {
            Ũ(true);
            if ((G_VAR32_Enum6 == Enum6.ʧ || G_VAR32_Enum6 == Enum6.ʦ) && ù() && Ȱ(G_VAR59_Vector3, Э.GetNaturalGravity()) < 25 && G_VAR63_int >= 2 && G_VAR64_int >= 2) {
                Vector3 ӑ = ɉ;
                if (ԓ > 0 && Ԓ < G_VAR64_int - 1)
                    ӑ = Ҽ(ԓ - 1, Ԓ + 1, true);
                else if (ԓ < G_VAR63_int - 1 && Ԓ < G_VAR64_int - 1)
                    ӑ = Ҽ(ԓ + 1, Ԓ + 1, true);
                else if (ԓ < G_VAR63_int - 1 && Ԓ > 0)
                    ӑ = Ҽ(ԓ + 1, Ԓ - 1, true);
                else if (ԓ > 0 && Ԓ > 0)
                    ӑ = Ҽ(ԓ - 1, Ԓ - 1, true);
                else
                    ΰ = true;

                if (!ΰ)
                    ñ(ӑ, 10);
            }
            else
                ΰ = true;
        }

        if (Ԍ < wpReachedDist / 2)
            ΰ = true;

        if (ΰ) {
            Ҝ(Enum10.Ҥ);
            return;
        }
    }

    if (G_VAR68_Enum10 == Enum10.ҥ || G_VAR68_Enum10 == Enum10.Ҥ) {
        if (Ԏ) {
            ñ(true, true, false, ɉ, 0);
            Ŗ(Ш, false);
            Ũ(true);
            ԉ = -1;
            Ԅ = G_VAR32_Enum6 == Enum6.ʥ || G_VAR32_Enum6 == Enum6.ʺ ? 0 : -1;
        }

        bool J = !Ĳ();
        int Ń = Ő("STONE", "ORE", Enum14.ņ);
        if (G_VAR32_Enum6 == Enum6.ʨ || G_VAR32_Enum6 == Enum6.ʺ || G_VAR32_Enum6 == Enum6.ʦ)
            Ń += Ő("ICE", "ORE", Enum14.ņ);

        bool Ӑ = Ń > 0;
        bool ɘ = false;
        if (Ԅ >= 0) {
            float Y = (float)Math.Sin(Ȩ(Ԅ)) * Й / 3f;
            float X = (float)Math.Cos(Ȩ(Ԅ)) * Я / 3f;
            Vector3 Ӝ = Ҽ(ԓ, Ԓ, true) + Ȏ(G_VAR59_Vector3, G_VAR60_Vector3 * -1, new Vector3(Y, X, 0));
            ñ(Ӝ, 0.3f);
            if (Ԍ < Math.Min(Й, Я) / 10f)
                Ԅ += 5f;

            if (Ԅ >= 360)
                Ԅ = 0;
        }

        if (ԉ == -1 || Ń < ԉ) {
            ԉ = Ń;
            ԅ = 0;
        } else {
            ԅ++;
            if (ԅ > 50)
                ɘ = true;
        }

        if (!Ӑ || J || ɘ) {
            if (!J) {
                int Ө = Ő("", "ORE", Enum14.ņ);
                if (Ű(true))
                    J = true;
                else if (100 - (µ(Ө, ԇ) * 100) < minEjection) {
                    J = true;
                } else
                    Ͻ(Enum2.Ђ);
            }

            if (ɘ && J)
                G_VAR12 = "Ejection failed";

            if (G_VAR68_Enum10 == Enum10.Ҥ) {
                if (J) {
                    if (G_VAR43_Class1.Ͷ)
                        Ҝ(Enum10.Ҧ);
                    else {
                        Ҕ();
                        Ґ();
                        G_VAR12 = "Can´t return, no dock found";
                    }
                } else
                    Ҝ(Enum10.ʟ);
            } else if (J)
                Ҝ(Enum10.ұ);
            else
                Ҝ(Enum10.Ҍ);

            return;
        }
    }

    if (G_VAR68_Enum10 == Enum10.ҝ || G_VAR68_Enum10 == Enum10.ұ || G_VAR68_Enum10 == Enum10.ң) {
        if (Ԏ) {
            ԍ = Ҽ(ԓ, Ԓ, false);
            ë(G_VAR58_Class1.ç, G_VAR58_Class1.Ï, G_VAR58_Class1.ĕ, false);
            Ŗ(Ш, G_VAR30_bool);
            Ũ(false);
            ԅ = Vector3.Distance(ɉ, ԍ);
            Ş(false, true, 0, Ю * sensorRange);
        }

        ñ(ԍ, Е(false));
        if (Vector3.Distance(ɉ, ԍ) >= ԅ + 5) {
            Ŗ(Ш, false);
            Ũ(true);
            G_VAR12 = "Can´t return!";
        }

        if (Ԍ < wpReachedDist) {
            if (G_VAR68_Enum10 == Enum10.ҝ && ԋ)
                Ҝ(Enum10.ҭ);

            if (G_VAR68_Enum10 == Enum10.ҝ)
                Ҝ(Enum10.ʟ);

            if (G_VAR68_Enum10 == Enum10.ң)
                Ҝ(Enum10.Ң);

            if (G_VAR68_Enum10 == Enum10.ұ) {
                if (G_VAR43_Class1.Ͷ)
                    Ҝ(Enum10.Ҧ);
                else {
                    Ҕ();
                    Ґ();
                    G_VAR12 = "Can´t return, no dock found";
                }
            }

            return;
        }
    }

    if (G_VAR68_Enum10 == Enum10.Ҧ) {
        if (Ԏ) {
            Ũ(true);
            Ŗ(Ш, false);
            int E = -1;
            double ӟ = -1;
            for (int A = G_VAR51_List_Class1.Count - 1; A >= 0; A--) {
                double Ʌ = Vector3.Distance(G_VAR51_List_Class1[A].ɉ, ɉ);
                if (ӟ == -1 || Ʌ < ӟ) {
                    E = A;
                    ӟ = Ʌ;
                }
            }

            if (E == -1) {
                Ҝ(Enum10.ү);
                return;
            }

            ĉ = G_VAR51_List_Class1[E].ɉ;
            ñ(ĉ, 10);
            ë(G_VAR58_Class1.ç, G_VAR58_Class1.Ï, G_VAR58_Class1.ĕ, false);
        }

        if (Ԍ < wpReachedDist) {
            Ҝ(Enum10.ү);
            return;
        }
    }

    if (G_VAR68_Enum10 == Enum10.ү || G_VAR68_Enum10 == Enum10.Ү) {
        if (G_VAR68_Enum10 == Enum10.Ү && G_VAR61_Enum9 == Enum9.Ӊ && ƽ != Enum13.Ͼ) {
            if (!Ĳ() || Ű(true)) {
                Ҝ(Enum10.ү);
                return;
            }
        }

        bool ΰ = false;
        bool Ӧ = false;
        bool ӥ = false;
        float Ӥ = 0;
        bool ӣ = false;
        Class1 B = null;
        if (Ԏ) {
            if (G_VAR68_Enum10 == Enum10.ү || ƽ == Enum13.Ͼ) {
                Class1 ҋ = ˡ();
                G_VAR71_Class2 = new Class2();
                G_VAR71_Class2.Ӻ = ҋ;
                G_VAR71_Class2.Ӹ = followPathDock * Ю;
                G_VAR71_Class2.ӷ = useDockDirectionDist * Ю;
                G_VAR71_Class2.Ӷ = 10;
                G_VAR71_Class2.ӹ.Add(ҋ.ɉ);
                Vector3 Ӣ = new Vector3();
                if (ˠ(ҋ, dockDist * Ю, true, out Ӣ))
                    G_VAR71_Class2.ӹ.Add(Ӣ);
                else
                    G_VAR71_Class2.Ӹ *= 1.5f;

                if (ƽ == Enum13.Ͼ) {
                    if (ҋ == G_VAR43_Class1)
                        G_VAR71_Class2.Ӵ = G_VAR58_Class1.ɉ;

                    if (ҋ == G_VAR58_Class1)
                        G_VAR71_Class2.Ӵ = G_VAR43_Class1.ɉ;

                    G_VAR71_Class2.ӵ = dockDist * Ю * 1.1f;
                }
            } else if (G_VAR68_Enum10 == Enum10.Ү) {
                G_VAR71_Class2 = new Class2();
                G_VAR71_Class2.Ӻ = G_VAR58_Class1;
                G_VAR71_Class2.Ӹ = followPathJob * Ю;
                G_VAR71_Class2.ӷ = useJobDirectionDist * Ю;
                G_VAR71_Class2.Ӷ = 10;
                G_VAR71_Class2.Ӵ = G_VAR43_Class1.ɉ;
                G_VAR71_Class2.ӵ = dockDist * Ю * 1.1f;
                G_VAR71_Class2.ӹ.Add(G_VAR58_Class1.ɉ);
                if (G_VAR61_Enum9 == Enum9.Ӌ) {
                    if (!G_VAR43_Class1.Ͷ || G_VAR51_List_Class1.Count == 0) {
                        Ҕ();
                        return;
                    }

                    float ӡ = Vector3.Distance(G_VAR51_List_Class1.First().ɉ, G_VAR43_Class1.ɉ);
                    float Ӡ = Vector3.Distance(G_VAR51_List_Class1.Last().ɉ, G_VAR43_Class1.ɉ);
                    if (ӡ < Ӡ)
                        G_VAR71_Class2.Ӻ = G_VAR51_List_Class1.Last();
                    else
                        G_VAR71_Class2.Ӻ = G_VAR51_List_Class1.First();
                }
            }

            Ԇ = new Vector3();
            ӣ = !ʼ(ɉ);
            Ŗ(Ш, false);
            Ũ(true);
            ԏ = -1;
            double ӟ = -1;
            for (int A = G_VAR51_List_Class1.Count - 1; A >= 0; A--) {
                if (Vector3.Distance(G_VAR51_List_Class1[A].ɉ, G_VAR71_Class2.Ӵ) <= G_VAR71_Class2.ӵ)
                    continue;

                double Ʌ = Vector3.Distance(G_VAR51_List_Class1[A].ɉ, ɉ);
                if (ӟ == -1 || Ʌ < ӟ) {
                    ԏ = A;
                    ӟ = Ʌ;
                }
            }

            ы = ˀ(G_VAR71_Class2.Ӻ.ɉ, ԏ);
            Ә = null;
        }

        н(G_VAR51_List_Class1, ы, G_VAR71_Class2.ӹ, G_VAR71_Class2.Ӹ, Ԏ, ref ԉ);
        for (int A = 0; A < G_VAR71_Class2.ӹ.Count; A++) {
            float Ʌ = Vector3.Distance(ɉ, G_VAR71_Class2.ӹ[A]);
            if (Ʌ <= G_VAR71_Class2.Ӹ)
                ΰ = true;

            if (Ʌ <= G_VAR71_Class2.ӷ)
                Ӧ = true;
        }

        if (Ӧ)
            Ӥ = G_VAR71_Class2.Ӷ;

        float Ӟ = Ә != null ? Ә.Ί : κ;
        float ӝ = (float)Math.Max(κ * 0.1f * Ю, wpReachedDist);
        if ((Ԍ < ӝ) || Ԏ) {
            if (!Ԏ)
                ԏ += ы;

            if (ы == 0 || ԏ > G_VAR51_List_Class1.Count - 1 || ԏ < 0)
                ΰ = true;
            else {
                Ԋ = ы > 0 ? G_VAR51_List_Class1.Count - 1 - ԏ : ԏ;
                B = G_VAR51_List_Class1[ԏ];
                Ә = B;
                if (ԏ >= 1 && ԏ < G_VAR51_List_Class1.Count - 1)
                    Ԇ = B.ɉ - G_VAR51_List_Class1[ԏ - ы].ɉ;
                else
                    Ә = null;

                ĉ = B.ɉ;
                ӥ = true;
            }
        }

        if (Ӧ)
            ë(G_VAR71_Class2.Ӻ.ç, G_VAR71_Class2.Ӻ.Ï, G_VAR71_Class2.Ӻ.ĕ, false);
        else if (ӣ)
            è(G_VAR71_Class2.Ӻ.ç,10, true);
        else if (ӥ && B != null)
            if (ы > 0)
                ë(B.ç, B.Ï, B.ĕ, 90, false);
            else ë(B.ç, -B.Ï, -B.ĕ, 90, false);

        ñ(true, false, true, ĉ, Ԇ, Ә == null ? 0 : Ә.Ί, Ӥ);
        if (ΰ) {
            Ԋ = 0;
            if (G_VAR68_Enum10 == Enum10.ү || ƽ == Enum13.Ͼ) {
                Ҝ(Enum10.Ҭ);
                return;
            }

            if (G_VAR68_Enum10 == Enum10.Ү && ԋ) {
                Ҝ(Enum10.ҭ);
                return;
            }

            if (G_VAR68_Enum10 == Enum10.Ү) {
                Ҝ(Enum10.ʟ);
                return;
            }
        }
    }

    if (G_VAR68_Enum10 == Enum10.Ҭ || G_VAR68_Enum10 == Enum10.ҩ) {
        Class1 ҋ = ˡ();
        if (Ԏ) {
            if (!ˠ(ҋ, dockDist * Ю, true, out ĉ)) {
                Ͻ(Enum2.Ё);
                Ҕ();
                return;
            }

            ñ(ĉ, 0);
            è(ҋ.ç, 90, true);
        }

        if (Ԍ < followPathDock * Ю && Ԍ != -1) {
            ñ(ĉ, 10);
            ë(ҋ.ç, ҋ.Ï, ҋ.ĕ, false);
        }

        if (Ò(MyShipConnectorStatus.Connectable) != null || Ò(MyShipConnectorStatus.Connected) != null) {
            Ҝ(Enum10.ґ);
            return;
        }

        if (Ԍ < wpReachedDist / 2 && Ԍ != -1) {
            Ҝ(Enum10.ҫ);
            return;
        }
    }

    if (G_VAR68_Enum10 == Enum10.ҫ || G_VAR68_Enum10 == Enum10.Ҫ) {
        if (Ԏ) {
            if (G_VAR68_Enum10 == Enum10.ҫ) {
                Class1 ҋ = ˡ();
                if (!ˠ(ҋ, dockDist * Ю, true, out ĉ)) {
                    Ͻ(Enum2.Ё);
                    Ҕ();
                    return;
                }

                ñ(true, true, false, ĉ, 0);
                ë(ҋ.ç, ҋ.Ï, ҋ.ĕ, 10, false);
            }

            if (G_VAR68_Enum10 == Enum10.Ҫ) {
                ë(G_VAR58_Class1.ç, G_VAR58_Class1.Ï, G_VAR58_Class1.ĕ, 0.5f, false);
                ĉ = G_VAR58_Class1.ɉ;
                ñ(true, true, false, ĉ, 0);
            }
        }

        if (ě) {
            á(false, 0, 0, 0, 0);
            if (G_VAR68_Enum10 == Enum10.ҫ)
                Ҝ(Enum10.ґ);

            if (G_VAR68_Enum10 == Enum10.Ҫ)
                Ҕ();

            return;
        }
    }

    if (G_VAR68_Enum10 == Enum10.ґ) {
        if (Ò(MyShipConnectorStatus.Connected) != null) {
            if (ƽ == Enum13.Ͼ)
                Ҝ(Enum10.ҡ);
            else
                Ҝ(Enum10.Ҩ);

            return;
        }

        Class1 ҋ = ˡ();
        if (Ԏ) {
            Ԅ = 0;
            ż = DateTime.Now;
            ԉ = 0;
            ë(ҋ.ç, ҋ.Ï, ҋ.ĕ, false);
        }

        Vector3I ѩ = new Vector3I((int)ҋ.Ή.X, (int)ҋ.Ή.Y, (int)ҋ.Ή.Z);
        IMySlimBlock ʣ = Me.CubeGrid.GetCubeBlock(ѩ);
        float к = dockingSpeed;
        float й = dockingSpeed * 5;
        float и = Math.Max(1.5f, Math.Min(5f, Ю * 0.15f));
        if (!ˠ(ҋ, 0, true, out ĉ) || !ˠ(ҋ, и, true, out Ԇ) || ʣ == null || !ʣ.FatBlock.IsFunctional) {
            Ͻ(Enum2.Ё);
            Ҕ();
            return;
        }

        if (Ԅ == 1 || (Vector3.Distance(ɉ, ĉ) <= и * 1.1f && !Ԏ))
            Ԅ = 1;
        else {
            Vector3 з = Ȑ(Э, Ԇ - ɉ);
            Vector3 ж = Ȑ(Э, Э.GetNaturalGravity());
            float Ƶ = ē(з, ж, null);
            к = Math.Min(й, Ƶ);
        }

        ñ(true, false, false, ĉ, ĉ - ɉ, dockingSpeed, к);
        if (Ԏ) ԅ = (float)Ԍ;
        IMyShipConnector Ä = Ò(MyShipConnectorStatus.Connectable);
        if (Ä != null) {
            ñ(false, false, false, ĉ, 0);
            if (ԉ > 0)
                ԉ = 0;

            ԉ--;
            if (ԉ < -5) {
                Ä.Connect();
                if (Ä.Status == MyShipConnectorStatus.Connected) {
                    if (ƽ == Enum13.Ͼ)
                        Ҝ(Enum10.ҡ);
                    else
                        Ҝ(Enum10.Ҩ);

                    å();
                    ť(Ч, true);
                    return;
                }
            }
        } else {
            float ț = (float)Math.Round(Ԍ, 1);
            if (ț < ԅ) {
                ԉ = -1;
                ԅ = ț;
            } else
                ԉ++;

            if (ԉ > 20) {
                Ҝ(Enum10.ҩ);
                return;
            }
        }
    }

    if (G_VAR68_Enum10 == Enum10.Ҩ || G_VAR68_Enum10 == Enum10.ҡ || G_VAR68_Enum10 == Enum10.Ĺ || G_VAR68_Enum10 == Enum10.ļ || G_VAR68_Enum10 == Enum10.Ŧ) {
        bool л = false;
        bool е = false;
        if (ƽ == Enum13.Ͼ) {
            if (ˡ() == G_VAR43_Class1)
                л = true;
            else if (ˡ() == G_VAR58_Class1)
                е = true;
        }

        if (Ԏ) {
            Ɔ = false;
            if (Ò(MyShipConnectorStatus.Connected) == null) {
                Ҝ(Enum10.ҧ);
                return;
            }

            å();
            if (л)
                ʘ(G_VAR19_Class5.Ɣ);

            if (е)
                ʘ(G_VAR20_Class5.Ɣ);

            ӛ = false;
            ә = false;
            Ӛ = false;
            ӧ = false;
        }

        if (Ò(MyShipConnectorStatus.Connected) == null) {
            Ҕ();
            Ͻ(Enum2.Ͽ);
            return;
        }

        if (G_VAR61_Enum9 != Enum9.Ӊ || G_VAR35_float == -1 || ĥ == Enum12.ɗ)
            ә = true;
        else if (Ĩ >= 100f)
            ә = true;
        else if (Ĩ <= 99f)
            ә = false;

        if (G_VAR61_Enum9 != Enum9.Ӊ || G_VAR37_float == -1 || Ф.Count == 0)
            Ӛ = true;
        else if (ļ >= 100f)
            Ӛ = true;
        else if (ļ <= 99)
            Ӛ = false;

        if (G_VAR61_Enum9 != Enum9.Ӊ || G_VAR36_float == -1 || Ц.Count == 0)
            ӧ = true;
        else
            ӧ = Ĺ >= G_VAR36_float;

        Class5 ƌ = null;
        if (л)
            ƌ = G_VAR19_Class5;

        if (е)
            ƌ = G_VAR20_Class5;

        if (ƌ != null && (ƌ.Ų == Enum15.Ÿ || ƌ.Ų == Enum15.ŷ))
            ә = true;

        if (ƌ != null && (ƌ.Ų == Enum15.Ź))
            if (!ӛ)
                ә = false;

        if (ƌ != null && (ƌ.Ų == Enum15.ŵ || ƌ.Ų == Enum15.Ŵ))
            Ӛ = true;

        if (ƌ != null && (ƌ.Ų == Enum15.Ŷ))
            if (!ӛ)
                Ӛ = false;

        if (ο) {
            ChargeMode д = ә ? ChargeMode.Auto : ChargeMode.Recharge;
            if (ƌ != null && (ƌ.Ų == Enum15.ŷ || ƌ.Ų == Enum15.Ÿ))
                д = ChargeMode.Discharge;

            ŧ(д); Ŕ(!Ӛ);
        }

        if (!ӛ) {
            if (ƽ == Enum13.Ͼ)
                ӛ = G_VAR61_Enum9 != Enum9.Ӊ || Ɖ(Ԏ, true) || G_VAR2;
            else
                ӛ = G_VAR61_Enum9 != Enum9.Ӊ || ş();
        } else {
            if (!ә)
                Ҝ(Enum10.Ŧ);

            if (!Ӛ)
                Ҝ(Enum10.ļ);

            if (!ӧ)
                Ҝ(Enum10.Ĺ);

            Ԏ = false;
        }

        if (ӛ && ә && Ӛ && ӧ) {
            ŧ(ChargeMode.Auto);
            Ŕ(false);
            if (G_VAR61_Enum9 == Enum9.Ӊ) {
                if (ƽ == Enum13.Ͼ) {
                    if (ˡ() == G_VAR43_Class1)
                        ʘ(G_VAR19_Class5.ƕ);
                    else if (ˡ() == G_VAR58_Class1)
                        ʘ(G_VAR20_Class5.ƕ);

                    if (ˡ() == G_VAR43_Class1)
                        ˣ(G_VAR58_Class1, Enum9.ӆ);
                    else
                        ˣ(G_VAR43_Class1, Enum9.Ӈ);
                }
            }

            Ҝ(Enum10.ҧ);
            return;
        }
    }

    if (G_VAR68_Enum10 == Enum10.ҧ) {
        if (Ԏ) {
            IMyShipConnector Ä = Ò(MyShipConnectorStatus.Connected);
            if (Ä == null) {
                Ҝ(Enum10.Ү);
                return;
            }

            IMyShipConnector г = Ä.OtherConnector;
            Ŗ(Ä, false);
            ť(Ч, false);
            Class1 B = null;
            if (Vector3.Distance(Ä.GetPosition(), G_VAR43_Class1.ɉ) < 5f && G_VAR43_Class1.Ͷ)
                B = G_VAR43_Class1;

            if (Vector3.Distance(Ä.GetPosition(), G_VAR58_Class1.ɉ) < 5f && G_VAR58_Class1.Ͷ)
                B = G_VAR58_Class1;

            if (B != null) {
                if (!ˠ(B, dockDist * Ю, true, out ĉ)) {
                    Ͻ(Enum2.Ё);
                    Ҕ();
                    return;
                }

                ñ(ĉ, 5);
                ë(B.ç, B.Ï, B.ĕ, false);
            } else
                ñ(ɉ + г.WorldMatrix.Forward * dockDist * Ю, 5);

            if (G_VAR61_Enum9 == Enum9.Ӊ)
                Ͻ(Enum2.Ђ);
        }

        if (Ԍ < wpReachedDist) {
            Ŗ(Ъ, true);
            Ҝ(Enum10.Ү);
            return;
        }
    }

    if (G_VAR68_Enum10 == Enum10.ҭ) {
        if (Ԏ) {
            Ũ(true);
            Ŗ(Ш, false);
            ĉ = G_VAR58_Class1.ɉ;
            ñ(ĉ, 20);
            ë(G_VAR58_Class1.ç, G_VAR58_Class1.Ï, G_VAR58_Class1.ĕ, false);
        }

        if (Ԍ < wpReachedDist / 2) {
            Ҝ(Enum10.Ҫ);
            return;
        }
    }

    Ԏ = false;
}

/**
 * Original: в
 */
class Class3 {
    public Class3(Vector3 б, float Ʌ) {
        this.б = б;
        this.Ʌ = Ʌ;
    }

    public Vector3 б;
    public float Ʌ;
}

void н(List<Class1> G_VAR51_List_Class1, int ы, List<Vector3> ъ, float Ʌ, bool Ç, ref int G_VAR3) {
    if (Ç) {
        for (int ã = 0; ã < G_VAR51_List_Class1.Count; ã++)
            G_VAR51_List_Class1[ã].Ί = 0;

        G_VAR3 = -1;
        return;
    }

    if (ы == 0)
        return;

    int щ = ы * -1;
    if (G_VAR3 == -1)
        G_VAR3 = щ > 0 ? 1 : G_VAR51_List_Class1.Count - 2;

    int Ř = 0;

    while (G_VAR3 >= 1 && G_VAR3 < G_VAR51_List_Class1.Count - 1) {
        if (Ř > 50)
            return;

        Ř++;
        try {
            if ((щ < 0 && G_VAR3 >= 1) || (щ > 0 && G_VAR3 <= G_VAR51_List_Class1.Count - 2)) {
                Class1 ʝ = G_VAR51_List_Class1[G_VAR3];
                bool ш = false;
                for (int ʡ = 0; ʡ < ъ.Count; ʡ++) {
                    if (Vector3.Distance(ʝ.ɉ, ъ[ʡ]) <= Ʌ) {
                        ш = true;
                        break;
                    }
                }

                if (!ш) {
                    Class1 ч = G_VAR51_List_Class1[G_VAR3 - щ];
                    Class1 ц = G_VAR51_List_Class1[G_VAR3 + щ];
                    Vector3 х = ʝ.ɉ - ц.ɉ;
                    Vector3 ф = ч.ɉ - ʝ.ɉ;
                    Vector3 у = ʝ.ɉ + Vector3.Normalize(х) * ф.Length();
                    Vector3 т = ч.ɉ - у;
                    Vector3 с = ȕ(ы > 0 ? ʝ.Ï : ʝ.Ï * -1, ʝ.ç * -1, т);
                    Vector3 р = ȕ(ы > 0 ? ʝ.Ï : ʝ.Ï * -1, ʝ.ç * -1, ф);
                    Vector3 п = ȕ(ы > 0 ? ʝ.Ï : ʝ.Ï * -1, ʝ.ç * -1, ʝ.Í);
                    ʝ.Ί = (float)Math.Sqrt(Math.Pow(ч.Ί, 2) + Math.Pow(ē(-р, п, ʝ), 2));
                    for (int ʡ = 0; ʡ < ъ.Count; ʡ++)
                        if (Vector3.Distance(ч.ɉ, ъ[ʡ]) <= Ʌ) {
                            Vector3 о = ȕ(ы > 0 ? ʝ.Ï : ʝ.Ï * -1, ʝ.ç * -1, ъ[ʡ] - ʝ.ɉ);
                            float ь = ē(-о, п, ʝ);
                            ʝ.Ί = Math.Min(ʝ.Ί, ь) / 2f;
                        }

                    if (с.Length() == 0)
                        с = new Vector3(0, 0, 1);

                    Vector3 а = ȕ(ʝ.Ï, ʝ.ç * -1, ʝ.Í);
                    float â = Â(с, а, ʝ);
                    float ª = µ(â, ϋ);
                    float K = (float)Math.Sqrt(с.Length() * 1.0f / (0.5f * ª));
                    ʝ.Ί = Math.Min(ʝ.Ί, (ф.Length() / K) * G_VAR38_float);
                }
            }
        }
        catch {
            return;
        }

        G_VAR3 += щ;
    }

    G_VAR3 = -1;
}

void З(bool Ç) {
    if (Ç) {
        G_VAR65_double = 0;
        return;
    }

    if (ƽ == Enum13.Ͼ)
        return;

    float ʝ = ԑ * Math.Max(1, G_VAR26_int);
    if (Ԑ == ԑ)
        ʝ += Math.Min(G_VAR26_int, ԁ);

    float ņ = G_VAR63_int * G_VAR64_int * Math.Max(1, G_VAR26_int);
    G_VAR65_double = Math.Max(G_VAR65_double, (float)Math.Min(ʝ / ņ * 100.0, 100));
}

MyDetectedEntityType Ж() {
    try {
        if (Ƃ(Ь, true) && !Ь.LastDetectedEntity.IsEmpty())
            return Ь.LastDetectedEntity.Type;
    }
    catch {

    };

    return MyDetectedEntityType.None;
}

float Е(bool Ï) {
    if (ƽ == Enum13.ψ && Ж() == MyDetectedEntityType.None && !ƃ(Ь, true))
        return fastSpeed;
    else
        return Ï ? G_VAR39_float : G_VAR40_float;
}

/**
 * Original: И
 */
public enum Enum12 {
    ɗ, Ŧ, Д, Г
}

/**
 * Original: В
 */
public enum Enum13 {
    ǻ, Б, ψ, А, Ͼ
}

Enum13 ƽ = Enum13.ǻ;
int ɘ = 0;
float Й = 0;
float Я = 0;
float Ю = 0;
IMyRemoteControl Э;
IMySensorBlock Ь;
List<IMyTimerBlock> Ы = new List<IMyTimerBlock>();
List<IMyShipConnector> Ъ = new List<IMyShipConnector>();
List<IMyThrust> Δ = new List<IMyThrust>();
List<IMyGyro> Щ = new List<IMyGyro>();
List<IMyTerminalBlock> Ш = new List<IMyTerminalBlock>();
List<IMyLandingGear> Ч = new List<IMyLandingGear>();
List<IMyReactor> Ц = new List<IMyReactor>();
List<IMyConveyorSorter> Х = new List<IMyConveyorSorter>();
List<IMyGasTank> Ф = new List<IMyGasTank>();
List<IMyTerminalBlock> У = new List<IMyTerminalBlock>();
List<IMyTerminalBlock> Т = new List<IMyTerminalBlock>();
List<IMyTerminalBlock> С = new List<IMyTerminalBlock>();
List<IMyBatteryBlock> Р = new List<IMyBatteryBlock>();
List<IMyTextPanel> П = new List<IMyTextPanel>();
List<IMyTextSurface> О = new List<IMyTextSurface>();
List<IMyTextPanel> Н = new List<IMyTextPanel>();
IMyTerminalBlock М = null;
bool Л(IMyTerminalBlock q) => q.CubeGrid == Me.CubeGrid;
void К() {
    GTS.GetBlocksOfType(У, Л);
}

void э() {
    Н.Clear();
    for (int A = П.Count - 1; A >= 0; A--) {
        String Ǩ = П[A].CustomData.ToUpper();
        bool Ѷ = false;
        if (Ǩ == ȣ) {
            Ѷ = true;
            G_VAR1 = true;
        }

        if (Ǩ == Ȣ)
            Ѷ = true;

        if (Ѷ) {
            Н.Add(П[A]);
            П.RemoveAt(A);
        }
    }

    Ţ(Н, false, 1, false);
}

void ѵ(List<IMyTerminalBlock> Ī) {
    О.Clear();
    for (int A = 0; A < Ī.Count; A++) {
        IMyTerminalBlock q = Ī[A];
        try {
            String љ = pamTag.Substring(0, pamTag.Length - 1) + ":";
            int E = q.CustomName.IndexOf(љ);
            int Ѵ = -1;
            if (E < 0 || !int.TryParse(q.CustomName.Substring(E + љ.Length, 1), out Ѵ))
                continue;

            if (Ѵ == -1)
                continue;

            Ѵ--;
            IMyTextSurfaceProvider ѳ = (IMyTextSurfaceProvider)q;
            if (Ѵ < ѳ.SurfaceCount && Ѵ >= 0) {
                О.Add(ѳ.GetSurface(Ѵ));
            }
        }
        catch {

        }
    }
}

void Ѳ() {
    if (Э == null)
        return;

    М = null;
    float ѱ = 0, Ѱ = 0, ѯ = 0, Ѯ = 0, ѭ = 0, Ѭ = 0;
    List<IMyTerminalBlock> ѫ = Ѧ(Ш, pamTag, true);
    bool Ѫ = ѫ.Count == 0;
    if (ѫ.Count > 0)
        М = ѫ[0];
    else if (Ш.Count > 0)
        М = Ш[0];

    int Ř = 0;
    for (int A = 0; A < Ш.Count; A++) {
        if (Ш[A].WorldMatrix.Forward != М.WorldMatrix.Forward) {
            if (Ѫ) {
                ɘ = 2; G_VAR12 = "Mining direction is unclear!";
                return;
            }

            continue;
        }

        Ř++;
        Vector3 ѷ = Ȓ(Э, Ш[A].GetPosition());
        if (A == 0) {
            ѱ = ѷ.X;
            Ѱ = ѷ.X;
            ѯ = ѷ.Y;
            Ѯ = ѷ.Y;
            ѭ = ѷ.Z;
            Ѭ = ѷ.Z;
        }

        Ѱ = Math.Max(ѷ.X, Ѱ);
        ѱ = Math.Min(ѷ.X, ѱ);
        Ѯ = Math.Max(ѷ.Y, Ѯ);
        ѯ = Math.Min(ѷ.Y, ѯ);
        Ѭ = Math.Max(ѷ.Z, Ѭ);
        ѭ = Math.Min(ѷ.Z, ѭ);
    }

    Й = (Ѱ - ѱ) * (1 - G_VAR41_float) + drillRadius * 2;
    Я = (Ѯ - ѯ) * (1 - G_VAR42_float) + drillRadius * 2;
    if (М != null && М.WorldMatrix.Forward == Э.WorldMatrix.Down)
        Я = (Ѭ - ѭ) * (1 - G_VAR42_float) + drillRadius * 2;
}

void Ѹ() {
    if (ɜ) {
        ɘ = 2;
        return;
    }

    List<IMyRemoteControl> ҁ = new List<IMyRemoteControl>();
    List<IMySensorBlock> Ҋ = new List<IMySensorBlock>();
    List<IMyTerminalBlock> ƣ = new List<IMyTerminalBlock>();
    GTS.GetBlocksOfType(ҁ, Л);
    GTS.GetBlocksOfType(П, Л);
    GTS.GetBlocksOfType(Ҋ, Л);
    GTS.SearchBlocksOfName(pamTag.Substring(0, pamTag.Length - 1) + ":", ƣ, q => q.CubeGrid == Me.CubeGrid && q is IMyTextSurfaceProvider);
    П = Ѧ(П, pamTag, true);
    э();
    ѵ(ƣ);
    Ţ(П, setLCDFontAndSize, 1.4f, false);
    Ţ(О, setLCDFontAndSize, 1.4f, true);
    List<IMySensorBlock> O = Ѧ(Ҋ, pamTag, true);
    if (O.Count > 0)
        Ь = O[0];
    else
        Ь = null;

    if (ƽ == Enum13.Б) {
        GTS.GetBlocksOfType(Ш, q => q.CubeGrid == Me.CubeGrid && q is IMyShipDrill);
        if (Ш.Count == 0) {
            ɘ = 1;
            G_VAR12 = "Drills are missing";
        }
    } else if (ƽ == Enum13.ψ) {
        GTS.GetBlocksOfType(Ш, q => q.CubeGrid == Me.CubeGrid && q is IMyShipGrinder);
        if (Ш.Count == 0) {
            ɘ = 1;
            G_VAR12 = "Grinders are missing";
        }

        if (ƽ == Enum13.ψ && Ь == null) {
            ɘ = 1;
            G_VAR12 = "Sensor is missing";
        }
    } else if (ƽ == Enum13.Ͼ) {
        GTS.GetBlocksOfType(Ы, q => q.CubeGrid == Me.CubeGrid);
    }

    List<IMyRemoteControl> Ķ = Ѧ(ҁ, pamTag, true);
    if (Ķ.Count > 0)
        ҁ = Ķ;

    if (ҁ.Count > 0)
        Э = ҁ[0];
    else {
        Э = null;
        ɘ = 2;
        G_VAR12 = "Remote is missing";
        return;
    }

    Ю = (float)Э.CubeGrid.WorldVolume.Radius * 2;
    М = null;
    if (ƽ != Enum13.Ͼ) {
        Ѳ();
        if (Ш.Count > 0 && М != null) {
            if (Ь != null && (М.WorldMatrix.Forward != Ь.WorldMatrix.Forward || !(Э.WorldMatrix.Forward == Ь.WorldMatrix.Up || Э.WorldMatrix.Down == Ь.WorldMatrix.Down))) {
                ɘ = 1;
                G_VAR12 = "Wrong sensor direction";
            }

            if (М.WorldMatrix.Forward != Э.WorldMatrix.Forward && М.WorldMatrix.Forward != Э.WorldMatrix.Down) {
                ɘ = 2;
                G_VAR12 = "Wrong remote direction";
            }
        }
    }
}

void Ҁ() {
    GTS.GetBlocksOfType(Ч, Л);
    GTS.GetBlocksOfType(Ъ, Л);
    GTS.GetBlocksOfType(Δ, Л);
    GTS.GetBlocksOfType(Щ, Л);
    GTS.GetBlocksOfType(Р, Л);
    GTS.GetBlocksOfType(Ц, Л);
    GTS.GetBlocksOfType(Ф, q => q.CubeGrid == Me.CubeGrid && q.BlockDefinition.ToString().ToUpper().Contains("HYDROGEN"));
    GTS.GetBlocksOfType(Х, Л);
    if (Me.CubeGrid.GridSizeEnum == MyCubeSize.Small)
        Ъ = Ѧ(Ъ, "ConnectorMedium", false);
    else
        Ъ = Ѧ(Ъ, "Connector", false);

    List<IMyShipConnector> ѿ = Ѧ(Ъ, pamTag, true);
    if (ѿ.Count > 0)
        Ъ = ѿ;

    if (ɘ <= 1) {
        if (Ъ.Count == 0) {
            ɘ = 1;
            G_VAR12 = "Connector is missing";
        }

        if (Щ.Count == 0) {
            ɘ = 1;
            G_VAR12 = "Gyros are missing";
        }

        if (Δ.Count == 0) {
            ɘ = 1;
            G_VAR12 = "Thrusters are missing";
        }
    }

    List<IMyConveyorSorter> Ѿ = Ѧ(Х, pamTag, true);
    if (Ѿ.Count > 0)
        Х = Ѿ;

    List<IMyLandingGear> ѽ = Ѧ(Ч, pamTag, true);
    if (ѽ.Count > 0)
        Ч = ѽ;

    for (int A = 0; A < Ч.Count; A++)
        Ч[A].AutoLock = false;

    List<IMyBatteryBlock> Ѽ = Ѧ(Р, pamTag, true);
    if (Ѽ.Count > 0)
        Р = Ѽ;

    List<IMyGasTank> ѻ = Ѧ(Ф, pamTag, true);
    if (ѻ.Count > 0)
        Ф = ѻ;
}

void Ѻ() {
    GTS.GetBlocksOfType(С, q => q.CubeGrid == Me.CubeGrid && q.InventoryCount > 0);
    Т.Clear();
    for (int A = С.Count - 1; A >= 0; A--) {
        if (Ѩ(С[A])) {
            Т.Add(С[A]);
            С.RemoveAt(A);
        }
    }
}

bool ѹ(IMyTerminalBlock q) {
    if (q.InventoryCount == 0)
        return false;

    if (ƽ == Enum13.Ͼ)
        return true;

    for (int ã = 0; ã < Ш.Count; ã++) {
        IMyTerminalBlock K = Ш[ã];
        if (K == null || !Ƃ(K, true) || K.InventoryCount == 0)
            continue;

        if (!checkConveyorSystem || K.GetInventory(0).IsConnectedTo(q.GetInventory(0))) {
            return true;
        }
    }

    return false;
}

bool Ѩ(IMyTerminalBlock q) {
    if (q is IMyCargoContainer)
        return true;

    if (q is IMyShipDrill)
        return true;

    if (q is IMyShipGrinder)
        return true;

    if (q is IMyShipConnector) {
        if (((IMyShipConnector)q).ThrowOut)
            return false;

        if (Me.CubeGrid.GridSizeEnum != MyCubeSize.Large && ї(q, "ConnectorSmall", false))
            return false;
        else
            return true;
    }

    return false;
}

List<ŕ> Ѧ<ŕ>(List<ŕ> Ī, String ј, bool і) {
    List<ŕ> J = new List<ŕ>();
    for (int A = 0; A < Ī.Count; A++)
        if (ї(Ī[A], ј, і))
            J.Add(Ī[A]);

    return J;
}

bool ї<ŕ>(ŕ Ō, String љ, bool і) {
    IMyTerminalBlock q = (IMyTerminalBlock)Ō;
    if (і && q.CustomName.ToUpper().Contains(љ.ToUpper()))
        return true;

    if (!і && q.BlockDefinition.ToString().ToUpper().Contains(љ.ToUpper()))
        return true;

    return false;
}

Dictionary<String, float> ѕ = new Dictionary<String, float>();
int є = 0;

void ѓ() {
    if (!G_VAR29_bool)
        return;

    if (Ш.Count <= 1)
        return;

    float ђ = 0;
    float ё = 0;
    for (int A = 0; A < Ш.Count; A++) {
        IMyTerminalBlock ѐ = Ш[A];
        if (ƃ(ѐ, true))
            continue;

        ђ += (float)ѐ.GetInventory(0).MaxVolume;
        ё += (float)ѐ.GetInventory(0).CurrentVolume;
    }

    float я = (float)Math.Round(µ(ё, ђ), 5);
    for (int A = 0; A < Math.Max(1, Math.Floor(Ш.Count / 10f)); A++) {
        float ю = 0;
        float њ = 0;
        float ѧ = 0;
        float ѥ = 0;
        IMyTerminalBlock ĝ = null;
        IMyTerminalBlock ȵ = null;
        for (int ã = 0; ã < Ш.Count; ã++) {
            IMyTerminalBlock ѐ = Ш[ã];
            if (ƃ(ѐ, true))
                continue;

            float Ѥ = (float)ѐ.GetInventory(0).MaxVolume;
            float ѣ = µ((float)ѐ.GetInventory(0).CurrentVolume, Ѥ);
            if (ĝ == null || ѣ > ю) {
                ĝ = ѐ;
                ю = ѣ;
                ѧ = Ѥ;
            }

            if (ȵ == null || ѣ < њ) {
                ȵ = ѐ;
                њ = ѣ;
                ѥ = Ѥ;
            }
        }

        if (ĝ == null || ȵ == null || ĝ == ȵ)
            return;

        if (checkConveyorSystem && !ĝ.GetInventory(0).IsConnectedTo(ȵ.GetInventory(0))) {
            if (є > 20)
                G_VAR12 = "Inventory balancing failed";
            else
                є++; return;
        }

        є = 0;
        List<MyInventoryItem> Ŏ = new List<MyInventoryItem>();
        ĝ.GetInventory(0).GetItems(Ŏ);
        float ѝ = 0;
        if (Ŏ.Count == 0)
            continue;

        MyInventoryItem Ĵ = Ŏ[0];
        String Ѣ = Ĵ.Type.TypeId + Ĵ.Type.SubtypeId;
        if (!ѕ.TryGetValue(Ѣ, out ѝ)) {
            if (џ(ĝ.GetInventory(0), 0, ȵ.GetInventory(0), out ѝ)) {
                ѕ.Add(Ѣ, ѝ);
            }
            else {
                return;
            }
        }

        float ѡ = ((ю - я) * ѧ / ѝ);
        float Ѡ = ((я - њ) * ѥ / ѝ);
        int Ń = (int)Math.Min(Ѡ, ѡ);
        if (Ń <= 0)
            return;

        if ((float)Ĵ.Amount < Ń)
            ĝ.GetInventory(0).TransferItemTo(ȵ.GetInventory(0), 0, null, null, Ĵ.Amount);
        else
            ĝ.GetInventory(0).TransferItemTo(ȵ.GetInventory(0), 0, null, null, Ń);
    }
}

bool џ(IMyInventory Ǡ, int E, IMyInventory ў, out float ѝ) {
    ѝ = 0;
    float ќ = (float)Ǡ.CurrentVolume;
    List<MyInventoryItem> ћ = new List<MyInventoryItem>();
    Ǡ.GetItems(ћ);
    float ƻ = 0;
    for (int A = 0; A < ћ.Count; A++)
        ƻ += (float)ћ[A].Amount;

    Ǡ.TransferItemTo(ў, E, null, null, 1);
    float ɱ = ќ - (float)Ǡ.CurrentVolume; ћ.Clear();
    Ǡ.GetItems(ћ);
    float ò = 0;
    for (int A = 0; A < ћ.Count; A++)
        ò += (float)ћ[A].Amount;

    if (ɱ == 0f || !Ȧ(0.9999, ƻ - ò, 1.0001)) {
        return false;
    }

    ѝ = ɱ; return true;
}

float ō(IMyTerminalBlock Ō, String Ô, String ń, String[] ŋ) {
    float J = 0;
    for (int ã = 0; ã < Ō.InventoryCount; ã++) {
        IMyInventory Ŋ = Ō.GetInventory(ã);
        List<MyInventoryItem> Ŏ = new List<MyInventoryItem>();
        Ŋ.GetItems(Ŏ);
        foreach (MyInventoryItem Ĵ in Ŏ) {
            if (ŋ != null && (ŋ.Contains(Ĵ.Type.TypeId.ToUpper()) || ŋ.Contains(Ĵ.Type.SubtypeId.ToUpper())))
                continue;

            if ((Ô == "" || Ĵ.Type.TypeId.ToUpper() == Ô) && (ń == "" || Ĵ.Type.SubtypeId.ToUpper() == ń))
                J += (float)Ĵ.Amount;
        }
    }

    return J;
}

/**
 * Original: ŉ
 */
public enum Enum14 {
    ň, Ň, ņ
}

/**
 * Original: Ņ
 */
class Class4 {
    public String Ô = "";
    public String ń = "";
    public int Ń = 0;
    public Enum14 ĭ = Enum14.ņ;
    public Class4(String Ô, String ń, int Ń, Enum14 ĭ) {
        this.Ô = Ô;
        this.ń = ń;
        this.Ń = Ń;
        this.ĭ = ĭ;
    }
}

Class4 Œ(String Ô, String ń, Enum14 ĭ, bool ő) {
    Ô = Ô.ToUpper();
    ń = ń.ToUpper();
    for (int A = 0; A < ŀ.Count; A++) {
        Class4 Ĵ = ŀ[A];
        if (Ĵ.Ô.ToUpper() == Ô && Ĵ.ń.ToUpper() == ń && (Ĵ.ĭ == ĭ || ĭ == Enum14.ņ))
            return Ĵ;
    }

    Class4 J = null;
    if (ő) {
        J = new Class4(Ô, ń, 0, ĭ);
        ŀ.Add(J);
    }

    return J;
}

int Ő(String Ô, String ń, Enum14 ĭ) {
    return Ő(Ô, ń, ĭ, null);
}

int Ő(String Ô, String ń, Enum14 ĭ, String[] ŋ) {
    int Ń = 0;
    Ô = Ô.ToUpper();
    ń = ń.ToUpper();
    for (int A = 0; A < ŀ.Count; A++) {
        Class4 Ĵ = ŀ[A];
        if (ŋ != null && ŋ.Contains(Ĵ.Ô.ToUpper()))
            continue;

        if ((Ô == "" || Ĵ.Ô.ToUpper() == Ô) && (ń == "" || Ĵ.ń.ToUpper() == ń) && (Ĵ.ĭ == ĭ || ĭ == Enum14.ņ))
            Ń += Ĵ.Ń;
    }

    return Ń;
}

float ŏ = 0; float œ = 0; float ł = 0; List<Class4> ŀ = new List<Class4>(); void Į(IMyTerminalBlock q, Enum14 ĭ) {
    for (int A = 0; A < q.InventoryCount; A++) {
        List<MyInventoryItem> Ĭ = new List<MyInventoryItem>(); q.GetInventory(A).GetItems(Ĭ);
        for (int ã = 0; ã < Ĭ.Count; ã++) {
            Œ(Ĭ[ã].Type.SubtypeId, Ĭ[ã].Type.TypeId.Replace("MyObjectBuilder_", ""), ĭ, true).Ń += (int)Ĭ[ã].Amount;
        }
    }
}

void ī(List<Class4> Ī) {
    for (int ĩ = Ī.Count - 1; ĩ > 0; ĩ--) {
        for (int A = 0; A < ĩ; A++) {
            Class4 ª = Ī[A];
            Class4 q = Ī[A + 1];
            if (ª.Ń < q.Ń)
                Ī.Move(A, A + 1);
        }
    }
}

void ħ() {
    try {
        ŀ.Clear();
        for (int A = 0; A < Т.Count; A++) {
            IMyTerminalBlock q = Т[A];
            if (!Ƃ(q, true))
                continue;

            Į(q, Enum14.ň);
        }

        if (G_VAR32_Enum6 != Enum6.ɗ) {
            for (int A = 0; A < С.Count; A++) {
                IMyTerminalBlock q = С[A];
                if (!Ƃ(q, true))
                    continue;

                Į(q, Enum14.Ň);
            }
        }

        ī(ŀ);
    } catch (Exception e) {
        ȥ = e;
    }
}

void Ħ() {
    ł = 0;
    œ = 0;
    try {
        for (int A = 0; A < Т.Count; A++) {
            IMyTerminalBlock q = Т[A];
            if (!Ƃ(q, true))
                continue;

            œ += (float)q.GetInventory(0).CurrentVolume;
            ł += (float)q.GetInventory(0).MaxVolume;
        }

        ŏ = (float)Math.Min(Math.Round(µ(œ, ł) * 100, 1), 100.0);
    } catch (Exception e) {
        ȥ = e;
    }
}

float Ĩ = 0;
Enum12 ĥ;

void İ() {
    float Ł = 0, Ŀ = 0, ľ = 0, Ľ = 0;
    for (int A = 0; A < Р.Count; A++) {
        IMyBatteryBlock q = Р[A];
        if (!Ƃ(q, true))
            continue;

        Ł += q.MaxStoredPower;
        Ŀ += q.CurrentStoredPower;
        ľ += q.CurrentInput;
        Ľ += q.CurrentOutput;
    }

    Ĩ = (float)Math.Round(µ(Ŀ, Ł) * 100, 1);
    if (ľ >= Ľ)
        ĥ = Enum12.Ŧ;
    else
        ĥ = Enum12.Г;

    if (ľ == 0 && Ľ == 0 || Ĩ == 100.0)
        ĥ = Enum12.Д;

    if (Р.Count == 0)
        ĥ = Enum12.ɗ;
}

float ļ = 0;

void Ļ() {
    float ĺ = 0;
    for (int A = 0; A < Ф.Count; A++) {
        IMyGasTank q = Ф[A];
        if (!Ƃ(q, true))
            continue;

        ĺ += (float)q.FilledRatio;
    }

    ļ = µ(ĺ, Ф.Count) * 100f;
}

float Ĺ = 0;
String ĸ = "";

void ķ() {
    Ĺ = 0;
    try {
        for (int A = 0; A < Ц.Count; A++) {
            IMyReactor Ķ = Ц[A];
            if (!Ƃ(Ķ, true))
                continue;
            List<MyInventoryItem> Ī = new List<MyInventoryItem>();
            Ķ.GetInventory(0).GetItems(Ī);
            float ĵ = 0;
            for (int ã = 0; ã < Ī.Count; ã++) {
                MyInventoryItem Ĵ = Ī[ã];
                if (Ĵ.Type.SubtypeId.ToUpper() == "URANIUM" && Ĵ.Type.TypeId.ToUpper().Contains("_INGOT"))
                    ĵ += (float)Ĵ.Amount;
            }

            if (ĵ < Ĺ || A == 0) {
                Ĺ = ĵ;
                ĸ = Ķ.CustomName;
            }
        }
    } catch (Exception e) {
        ȥ = e;
    }
}

void ĳ() {
    if (G_VAR66_bool) {
        if (į().Count > G_VAR67_int) {
            G_VAR66_bool = false;
            if (G_VAR21_Enum5 != Enum5.ʫ) {
                Ҕ();
                if (G_VAR21_Enum5 == Enum5.ʭ)
                    Ґ();

                if (G_VAR21_Enum5 == Enum5.ʮ)
                    if (G_VAR43_Class1.Ͷ)
                        ҏ();
                    else
                        Ґ();
            }

            G_VAR12 = "Damage detected";
        }
    }
}

bool Ĳ() {
    if (!λ)
        return true;

    if (G_VAR61_Enum9 == Enum9.Ӊ) {
        if (G_VAR35_float > 0 && ĥ != Enum12.ɗ) {
            if (Ĩ <= G_VAR35_float) {
                G_VAR12 = "Low energy! Move home";
                return false;
            }
        }

        if (G_VAR36_float > 0 && Ц.Count > 0) {
            if (Ĺ <= G_VAR36_float) {
                G_VAR12 = "Low fuel: " + ĸ;
                return false;
            }
        }

        if (G_VAR37_float > 0 && Ф.Count > 0) {
            if (ļ <= G_VAR37_float) {
                G_VAR12 = "Low hydrogen";
                return false;
            }
        }
    }

    return true;
}

List<IMyTerminalBlock> į() {
    List<IMyTerminalBlock> ı = new List<IMyTerminalBlock>();
    for (int A = 0; A < У.Count; A++) {
        IMyTerminalBlock q = У[A];
        if (ƃ(q, false))
            ı.Add(q);
    }

    return ı;
}

bool ƃ(IMyTerminalBlock Ō, bool Ɓ) {
    return (!Ƃ(Ō, Ɓ) || !Ō.IsFunctional);
}

bool Ƃ(IMyTerminalBlock q, bool Ɓ) {
    if (q == null)
        return false;

    try {
        IMyCubeBlock ƀ = Me.CubeGrid.GetCubeBlock(q.Position).FatBlock;
        if (Ɓ)
            return ƀ == q;
        else
            return ƀ.GetType() == q.GetType();
    } catch {
        return false;
    }
}

/**
 * Original: ſ
 */
public enum Enum15 {
    ž, Ž, ż, Ż, ź, Ź, Ÿ, ŷ, Ŷ, ŵ, Ŵ
}

/**
 * Original: ų
 */ 
class Class5 {
    public Enum15 Ų = Enum15.ž;
    public float Ƅ = 0;
    public float ƅ = 0;
    public string Ɣ = "";
    public string ƕ = "";
    DateTime Ɠ;
    public bool ƒ = false;
    private bool Ƒ = false;
    public bool Ɛ(bool Ç) {
        if (Ç) {
            ƅ = 0;
            Ƒ = false;
            return false;
        }

        Ƒ = true;
        return ƅ > Ƅ;
    }

    public void Ç() {
        Ɛ(true);
        ƒ = false;
    }

    public void Ə() {
        if (Ƒ)
            if ((DateTime.Now - Ɠ).TotalSeconds > 1) {
                ƅ++;
                Ɠ = DateTime.Now;
            }
    }

    public bool Ǝ() {
        switch (Ų) {
            case Enum15.Ž:
                return true;

            case Enum15.ż:
                return true;
        }

        return false;
    }
}

bool ƍ(Class5 ƌ, bool Ç, bool ƈ) {
    if (Ç)
        ƌ.Ç();

    ƌ.Ə();

    bool J = false;
    String O = "";
    switch (ƌ.Ų) {
        case Enum15.ž:
            {
                O = "Waiting for command";
                J = false;
                break;
            }

        case Enum15.Ż:
            {
                O = "Waiting for cargo";
                J = Ű(true);
                break;
            }

        case Enum15.ź:
            {
                O = "Unloading";
                J = ş();
                break;
            }

        case Enum15.ż:
            {
                J = true;
                break;
            }

        case Enum15.Ź:
            {
                O = "Charging batteries";
                J = Ĩ >= 100f;
                break;
            }

        case Enum15.Ÿ:
            {
                O = "Discharging batteries";
                J = Ĩ <= 25f;
                break;
            }
        case Enum15.ŷ:
            {
                O = "Discharging batteries";
                J = Ĩ <= 0f;
                break;
            }

        case Enum15.Ŷ:
            {
                O = "Filling up hydrogen";
                J = ļ >= 100f;
                break;
            }

        case Enum15.ŵ:
            {
                O = "Unloading hydrogen";
                J = ļ <= 25f;
                break;
            }

        case Enum15.Ŵ:
            {
                O = "Unloading hydrogen";
                J = ļ <= 0f;
                break;
            }

        case Enum15.Ž:
            {
                bool Ƌ = ů();
                if (!Ƌ)
                    ƌ.ƒ = true;

                J = ƌ.ƒ && Ƌ;
                O = "Waiting for passengers";
                break;
            }
    }

    if (!J)
        ƌ.Ɛ(true);

    if (J && ƌ.Ǝ()) {
        J = ƌ.Ɛ(false);
        O = "Undocking in: " + Ǖ((int)Math.Max(0, ƌ.Ƅ - ƌ.ƅ));
    }

    if (ƈ) Ɗ = O;
    return J;
}

String Ɗ = "";

bool Ɖ(bool Ç, bool ƈ) {
    IMyShipConnector Ä = Ò(MyShipConnectorStatus.Connected);
    if (Ä == null)
        return false;

    if (Vector3.Distance(G_VAR43_Class1.ɉ, Ä.GetPosition()) < 5)
        return ƍ(G_VAR19_Class5, Ç, ƈ);

    if (Vector3.Distance(G_VAR58_Class1.ɉ, Ä.GetPosition()) < 5)
        return ƍ(G_VAR20_Class5, Ç, ƈ);

    return false;
}

float Ƈ = 0;
bool Ɔ = false;

bool Ű(bool š) {
    if (G_VAR28_bool && ƽ != Enum13.Ͼ)
        if (Ƈ != -1 && ϋ >= Ƈ) {
            G_VAR12 = "Ship too heavy";
            return true;
        }

    if (ŏ >= G_VAR34_float || Ɔ) {
        Ɔ = false;
        G_VAR12 = "Ship is full";
        return true;
    }

    return false;
}

bool ů() {
    List<IMyCockpit> Ī = new List<IMyCockpit>();
    GTS.GetBlocksOfType(Ī, q => q.CubeGrid == Me.CubeGrid);
    for (int A = 0; A < Ī.Count; A++)
        if (Ī[A].IsUnderControl)
            return true;

    return false;
}

bool ş() {
    String[] ŋ = null;
    if (!G_VAR33_bool)
        ŋ = new string[] { "ICE" };

    if (ƽ == Enum13.Б)
        return Ő("", "ORE", Enum14.ň, ŋ) == 0;

    if (ƽ == Enum13.ψ)
        return Ő("", "COMPONENT", Enum14.ň, ŋ) == 0;
    else
        return Ő("", "", Enum14.ň, ŋ) == 0;
}

void Ş(bool ŝ, bool Ŝ, float ś, float Š) {
    if (Ь == null || Ш.Count == 0)
        return;

    Vector3 Ś = new Vector3();
    int Ř = 0;
    for (int A = 0; A < Ш.Count; A++) {
        if (Ш[A].WorldMatrix.Forward != М.WorldMatrix.Forward)
            continue;

        Ř++;
        Ś += Ш[A].GetPosition();
    }

    Ś = Ś / Ř;
    Vector3 ŗ = Ȓ(Ь, Ś);
    Ь.Enabled = true;
    Ь.ShowOnHUD = ŝ;
    Ь.LeftExtend = (Ŝ ? 1 : G_VAR24_int) / 2f * Й - ŗ.X;
    Ь.RightExtend = (Ŝ ? 1 : G_VAR24_int) / 2f * Й + ŗ.X;
    Ь.TopExtend = (Ŝ ? 1 : G_VAR25_int) / 2f * Я + ŗ.Y;
    Ь.BottomExtend = (Ŝ ? 1 : G_VAR25_int) / 2f * Я - ŗ.Y;
    Ь.FrontExtend = (ŝ ? G_VAR26_int : ś) - ŗ.Z;
    Ь.BackExtend = ŝ ? 0 : Š + Ю * 0.75f + ŗ.Z;
    Ь.DetectFloatingObjects = true;
    Ь.DetectAsteroids = false;
    Ь.DetectLargeShips = true;
    Ь.DetectSmallShips = true;
    Ь.DetectStations = true;
    Ь.DetectOwner = true;
    Ь.DetectSubgrids = false;
    Ь.DetectPlayers = false;
    Ь.DetectEnemy = true;
    Ь.DetectFriendly = true;
    Ь.DetectNeutral = true;
}

void Ŗ<ŕ>(List<ŕ> Ă, bool e) {
    for (int A = 0; A < Ă.Count; A++)
        Ŗ((IMyTerminalBlock)Ă[A], e);
}

void Ŕ(bool ř) {
    for (int A = 0; A < Ф.Count; A++) {
        Ф[A].Stockpile = ř;
    }
}

void Ţ<ŕ>(List<ŕ> Ă, bool Ů, float ŭ, bool Ŭ) {
    for (int A = 0; A < Ă.Count; A++) {
        IMyTextSurface O = null;
        if (Ă[A] is IMyTextSurface)
            O = (IMyTextSurface)Ă[A];

        if (O != null) {
            O.ContentType = ContentType.TEXT_AND_IMAGE;
            if (!Ů)
                continue;

            O.Font = "Debug";
            if (Ŭ)
                continue;

            O.FontSize = ŭ;
        }
    }
}

void Ŗ(IMyTerminalBlock q, bool e) {
    if (q == null)
        return;

    String ū = e ? "OnOff_On" : "OnOff_Off";
    var Ū = q.GetActionWithName(ū);
    Ū.Apply(q);
}

bool ũ = true;
void Ũ(bool e) {
    ũ = e;
    if (!G_VAR31_bool)
        return;

    Ŗ(Х, e);
}

void ŧ(ChargeMode Ŧ) {
    for (int A = 0; A < Р.Count; A++)
        Р[A].ChargeMode = Ŧ;
}

void ť(List<IMyLandingGear> q, bool Ť) {
    for (int A = 0; A < q.Count; A++) {
        if (Ť)
            q[A].Lock();

        if (!Ť)
            q[A].Unlock();
    }
}

bool ţ = false;

void ű(bool e) {
    if (ţ == e)
        return;

    List<IMyShipController> Ă = new List<IMyShipController>();
    GTS.GetBlocksOfType(Ă, Л);
    if (Ă.Count == 0)
        return;

    for (int A = 0; A < Ă.Count; A++)
        Ă[A].DampenersOverride = e;

    ţ = e;
}

IMyShipConnector Ò(MyShipConnectorStatus Ñ) {
    for (int A = 0; A < Ъ.Count; A++) {
        if (!Ƃ(Ъ[A], true))
            continue;

        if (Ъ[A].Status == Ñ)
            return Ъ[A];
    }

    return null;
}

float Ð(Vector3 Ï, Vector3 Î, Vector3 Í, Class1 B) {
    if (Í.Length() == 0f)
        return 0;

    Vector3 Ó = ȕ(Ï, Î, Vector3.Normalize(Í));
    float Ì = Â(-Ó, B);
    return Ì / Í.Length();
}

int Ê = 0;
Class1 É = null;

void È(bool Ç) {
    float Æ = 0;
    float Å = 0.9f;
    if (Ç) {
        Ƈ = -1;
        Ê = 0;
        É = null;
        if (G_VAR61_Enum9 != Enum9.Ӌ && G_VAR58_Class1.Í.Length() != 0) {
            Æ = Å * Ð(G_VAR58_Class1.Ï, G_VAR58_Class1.ç * -1, G_VAR58_Class1.Í, null);
            if (Æ < Ƈ || Ƈ == -1)
                Ƈ = Æ;
        }

        if (G_VAR43_Class1.Ͷ && G_VAR43_Class1.Í.Length() != 0) {
            Æ = Å * Ð(G_VAR43_Class1.Ï, G_VAR43_Class1.ç * -1, G_VAR43_Class1.Í, null);
            if (Æ < Ƈ || Ƈ == -1)
                Ƈ = Æ;
        }

        return;
    }

    if (Ê == -1)
        return;

    if (Ê >= 0) {
        int Ä = 0;
        while (Ê < G_VAR51_List_Class1.Count) {
            if (Ä > 100)
                return;

            Ä++;
            Class1 B = G_VAR51_List_Class1[Ê];
            if (B.Í.Length() != 0f) {
                Æ = Å * Math.Min(Ð(B.Ï, B.ç * -1, B.Í, B), Ð(B.Ï * -1, B.ç * -1, B.Í, B));
                if (Æ < Ƈ || Ƈ == -1)
                    Ƈ = Æ;
            } else
                É = B;

            Ê++;
        }

        Ê = -1;
    }

    bool Ã = true;
    float Ë = 0;
    if (G_VAR51_List_Class1.Count == 0 && Ƈ == -1)
        Ã = false;

    if (É != null) {
        for (int K = 0; K < ä.Count; K++) {
            String U = ä.Keys.ElementAt(K);
            float[,] I = ä.Values.ElementAt(K);
            float F = 0;
            if (!H(É, U, out F)) {
                Ã = false;
                break;
            }

            for (int A = 0; A < I.GetLength(0); A++) {
                for (int ã = 0; ã < I.GetLength(1); ã++) {
                    float â = Math.Abs(I[A, ã] * F);
                    if (â == 0)
                        continue;

                    Ã = true;
                    if (Ë == 0 || â < Ë)
                        Ë = â;
                }
            }
        }
    }

    if (!Ã) {
        for (int A = 0; A < Õ.GetLength(0); A++) {
            for (int ã = 0; ã < Õ.GetLength(1); ã++) {
                float â = Math.Abs(Õ[A, ã]);
                if (â == 0)
                    continue;

                if (Ë == 0 || â < Ë)
                    Ë = â;
            }
        }
    }

    if (Ë > 0) {
        Æ = µ(Ë, Me.CubeGrid.GridSizeEnum == MyCubeSize.Small ? minAccelerationSmall : minAccelerationLarge);
        if (Æ > 0)
            if (Æ < Ƈ || Ƈ == -1)
                Ƈ = Æ;
    }
}

void á(bool à, float ß, float Þ, float Ý, float Ü) {
    for (int A = 0; A < Щ.Count; A++) {
        IMyGyro Û = Щ[A];
        Û.GyroOverride = à;
        if (!à)
            Û.GyroPower = 100;
        else
            Û.GyroPower = ß;

        if (!à)
            continue;

        Vector3 Ï = Э.WorldMatrix.Forward;
        Vector3 Ú = Э.WorldMatrix.Right;
        Vector3 Î = Э.WorldMatrix.Up;
        Vector3 Ù = Û.WorldMatrix.Forward;
        Vector3 Ø = Û.WorldMatrix.Up;
        Vector3 Ö = Û.WorldMatrix.Left * -1;
        if (Ù == Ï)
            Û.SetValueFloat("Roll", Ü);
        else if (Ù == (Ï * -1))
            Û.SetValueFloat("Roll", Ü * -1);
        else if (Ø == (Ï * -1))
            Û.SetValueFloat("Yaw", Ü);
        else if (Ø == Ï)
            Û.SetValueFloat("Yaw", Ü * -1);
        else if (Ö == Ï)
            Û.SetValueFloat("Pitch", Ü);
        else if (Ö == (Ï * -1))
            Û.SetValueFloat("Pitch", Ü * -1);

        if (Ö == (Ú * -1))
            Û.SetValueFloat("Pitch", Þ);
        else if (Ö == Ú)
            Û.SetValueFloat("Pitch", Þ * -1);
        else if (Ø == Ú)
            Û.SetValueFloat("Yaw", Þ);
        else if (Ø == (Ú * -1))
            Û.SetValueFloat("Yaw", Þ * -1);
        else if (Ù == (Ú * -1))
            Û.SetValueFloat("Roll", Þ);
        else if (Ù == Ú)
            Û.SetValueFloat("Roll", Þ * -1);

        if (Ø == (Î * -1))
            Û.SetValueFloat("Yaw", Ý);
        else if (Ø == Î)
            Û.SetValueFloat("Yaw", Ý * -1);
        else if (Ö == Î)
            Û.SetValueFloat("Pitch", Ý);
        else if (Ö == (Î * -1))
            Û.SetValueFloat("Pitch", Ý * -1);
        else if (Ù == Î)
            Û.SetValueFloat("Roll", Ý);
        else if (Ù == (Î * -1))
            Û.SetValueFloat("Roll", Ý * -1);
    }
}

float[,] Õ = new float[3, 2];
Dictionary<String, float[,]> ä = new Dictionary<string, float[,]>();

void D(IMyTerminalBlock q) {
    if (q == null)
        return;

    Õ = new float[3, 2];
    ä = new Dictionary<string, float[,]>();
    for (int A = 0; A < Δ.Count; A++) {
        IMyThrust K = Δ[A];
        if (!K.IsFunctional)
            continue;

        Vector3 Q = Ȑ(q, K.WorldMatrix.Backward);
        float P = K.MaxEffectiveThrust;
        if (Math.Round(Q.X, 2) != 0.0)
            if (Q.X >= 0)
                Õ[0, 0] += P;
            else
                Õ[0, 1] -= P;

        if (Math.Round(Q.Y, 2) != 0.0)
            if (Q.Y >= 0)
                Õ[1, 0] += P;
            else
                Õ[1, 1] -= P;

        if (Math.Round(Q.Z, 2) != 0.0)
            if (Q.Z >= 0)
                Õ[2, 0] += P;
            else
                Õ[2, 1] -= P;

        String O = L(K);
        float[,] N = null;
        if (ä.ContainsKey(O))
            N = ä[O];
        else {
            N = new float[3, 2];
            ä.Add(O, N);
        }

        float M = K.MaxThrust;
        if (Math.Round(Q.X, 2) != 0.0)
            if (Q.X >= 0)
                N[0, 0] += M;
            else
                N[0, 1] -= M;

        if (Math.Round(Q.Y, 2) != 0.0)
            if (Q.Y >= 0)
                N[1, 0] += M;
            else
                N[1, 1] -= M;

        if (Math.Round(Q.Z, 2) != 0.0)
            if (Q.Z >= 0)
                N[2, 0] += M;
            else
                N[2, 1] -= M;
    }
}

static String L(IMyThrust K) {
    return K.BlockDefinition.SubtypeId;
}

Vector3 S(Vector3 C, float[,] I) {
    return new Vector3(
        C.X >= 0 ? I[0, 0] : I[0, 1], 
        C.Y >= 0 ? I[1, 0] : I[1, 1],
        C.Z >= 0 ? I[2, 0] : I[2, 1]
        );
}

bool H(Class1 B, String G, out float F) {
    F = 0;
    int E = G_VAR50_List_String.IndexOf(G);
    if (E == -1 || B.Θ == null || E >= B.Θ.Length)
        return false;

    F = B.Θ[E];
    if (F == -1)
        return false;

    return true;
}

Vector3 D(Vector3 C, Class1 B) {
    if (B != null) {
        Vector3 J = new Vector3();
        for (int A = 0; A < ä.Keys.Count; A++) {
            String U = ä.Keys.ElementAt(A);
            float F = 0;
            if (!H(B, U, out F)) {
                return S(C, Õ);
            }

            J += S(C, ä.Values.ElementAt(A)) * F;
        }

        return J;
    }

    return S(C, Õ);
}

float Â(Vector3 C, Class1 B) {
    return Â(C, new Vector3(), B);
}

float Â(Vector3 C, Vector3 Á, Class1 B) {
    Vector3 Z = D(C, B);
    Vector3 À = Z + Á * ϋ;
    float º = (À / C).AbsMin();
    return (float)(C * º).Length();
}

static float µ(float ª, float q) {
    if (q == 0)
        return 0;

    return ª / q;
}

void k(Vector3 h, bool e) {
    if (!e) {
        for (int A = 0; A < Δ.Count; A++)
            Δ[A].SetValueFloat("Override", 0.0f);

        return;
    }

    Vector3 Z = D(h, null);
    float Y = Math.Min(1, Math.Abs(µ(h.X, Z.X)));
    float X = Math.Min(1, Math.Abs(µ(h.Y, Z.Y)));
    float V = Math.Min(1, Math.Abs(µ(h.Z, Z.Z)));
    for (int A = 0; A < Δ.Count; A++) {
        IMyThrust K = Δ[A];
        Vector3 Q = ȴ(Ȑ(Э, K.WorldMatrix.Backward), 1);
        if (Q.X != 0 && Math.Sign(Q.X) == Math.Sign(h.X))
            K.SetValueFloat("Override", K.MaxThrust * Y);
        else if (Q.Y != 0 && Math.Sign(Q.Y) == Math.Sign(h.Y))
            K.SetValueFloat("Override", K.MaxThrust * X);
        else if (Q.Z != 0 && Math.Sign(Q.Z) == Math.Sign(h.Z))
            K.SetValueFloat("Override", K.MaxThrust * V);
        else
            K.SetValueFloat("Override", 0.0f);
    }
}

float ē(Vector3 đ, Vector3 Đ, Class1 B) {
    if (đ.Length() == 0)
        return 0;

    float Å = 1;
    if (Đ.Length() > 0)
        Å = Math.Min(1, Ȱ(-Đ, đ) / 90) * 0.4f + 0.6f;

    float ď = Â(đ, Đ, B);
    if (ď == 0)
        return 0.1f;

    float ª = µ(ď, ϋ);
    float K = (float)Math.Sqrt(µ(đ.Length(), ª * 0.5f));

    return ª * K * Å * G_VAR38_float;
}

bool Ď = false;
bool č = false;
bool Č = false;
bool ċ = false;
float Ċ = 0;
float û = 0;
Vector3 ü = new Vector3();
Vector3 ĉ = new Vector3();
Vector3 Ĉ = new Vector3(1, 1, 1);
float ć = 1;
Vector3 Ć = new Vector3();

void ą() {
    Vector3 Ą = ĉ - ɉ;
    if (Ą.Length() == 0)
        Ą = new Vector3(0, 0, -1);

    Vector3 ă = Ȑ(Э, Ą);
    Vector3 Ē = Vector3.Normalize(ă);
    Vector3 Á = Ȑ(Э, Э.GetNaturalGravity());
    float ģ = û > 0 ? Math.Max(0, 1 - Ȱ(Ą, ü) / 5) : 0;
    float Ĥ = (float)Math.Min((Ċ > 0 ? Ċ : 1000f), Math.Max(ē(-ă, Á, null), û * ģ));
    if (!Ď) Ĥ = 0; if (č) Ĥ = Math.Max(0, 1 - ė / Ė) * Ĥ;
    if (generalSpeedLimit > 0)
        Ĥ = Math.Min(generalSpeedLimit, Ĥ);

    if (ċ)
        Ĥ *= (float)Math.Min(1, µ(Ą.Length(), wpReachedDist) /2);

    Vector3 Ģ = Ȑ(Э, Э.GetShipVelocities().LinearVelocity);
    float ġ = (float)(Math.Max(0, 15 - Ȱ(-Ē, -Ģ)) / 15) * 0.85f + 0.15f;
    ć += Math.Sign(ġ - ć) / 10f;
    Vector3 Ġ = Ē * Ĥ * ć - (Ģ);
    Vector3 ĝ = D(Ġ, null);
    if (Č && Ԍ > 0.1f) {
        Ġ.X *= ğ(Ġ.X, ref Ĉ.X, 1f, ĝ.X, 20);
        Ġ.Y *= ğ(Ġ.Y, ref Ĉ.Y, 1f, ĝ.Y, 20);
        Ġ.Z *= ğ(Ġ.Z, ref Ĉ.Z, 1f, ĝ.Z, 20);
    } else
        Ĉ = new Vector3(1, 1, 1);

    Ć = ϋ * Ġ - Á * ϋ;
    k(Ć, Č);
    Ԍ = Vector3.Distance(ɉ, ĉ);
}

float ğ(float ª, ref float ğ, float Ğ, float ĝ, float Ĝ) {
    ª = Math.Sign(Math.Round(ª, 2));
    if (ª == Math.Sign(ğ))
        ğ += Math.Sign(ğ) * Ğ;
    else
        ğ = ª;

    if (ª == 0)
        ğ = 1;

    float J = Math.Abs(ğ);
    if (J < Ĝ || ĝ == 0)
        return J;

    ğ = Math.Min(Ĝ, Math.Max(-Ĝ, ğ));
    J = Math.Abs(ĝ);
    return J;
}

bool ě = false;
bool Ě = false;
bool ę = false;
bool Ę = false;
float ė = 0;
float Ė = 2;
Vector3 ĕ;
Vector3 Ï;
Vector3 ç;

void Ĕ() {
    float Þ = 90;
    float Ü = 90;
    float Ý = 90;
    float ā = (float)(Me.CubeGrid.GridSizeEnum == MyCubeSize.Small ? gyroSpeedSmall : gyroSpeedLarge) / 100f;
    Vector3 ð;
    Vector3 ï;
    Vector3 î;
    if (ę) {
        ð = Vector3.Normalize(ĉ - ɉ);
        ï = Ȑ(Э, ð); î = Ȑ(Э, ç);
        Þ = Ȱ(ï, new Vector3(0, -1, 0)) - 90;
        Ü = Ȭ(î, new Vector3(-1, 0, 0), î.Y);
        Ý = Ȭ(ï, new Vector3(-1, 0, 0), ï.Z);
    } else {
        ð = Ï;
        î = Ȑ(Э, ç);
        ï = Ȑ(Э, Ï);
        Vector3 í = Ȑ(Э, ĕ);
        Þ = Ȭ(î, new Vector3(0, 0, 1), î.Y);
        Ü = Ȭ(î, new Vector3(-1, 0, 0), î.Y);
        Ý = Ȭ(í, new Vector3(0, 0, 1), í.X);
    }

    if (Ę && ù()) {
        Vector3 Í = Э.GetNaturalGravity();
        î = Ȑ(Э, Í);
        Þ = Ȭ(î, new Vector3(0, 0, 1), î.Y);
        Ü = Ȭ(î, new Vector3(-1, 0, 0), î.Y);
    }

    if (!Ȧ(-45, Ü, 45)) {
        Þ = 0; Ý = 0;
    };

    if (!Ȧ(-45, Ý, 45))
        Þ = 0;

    á(Ě, 1, (-Þ) * ā, (-Ý) * ā, (-Ü) * ā);
    ė = Math.Max(Math.Abs(Þ), Math.Max(Math.Abs(Ü), Math.Abs(Ý)));
    ě = ė <= Ė;
}

void ì() {
    this.Ě = false;
}

void ë(Vector3 ç, Vector3 Ï, Vector3 ê, float æ, bool é) {
    è(ç, æ, é);
    Ė = æ;
    ę = false;
    this.Ï = Ï;
    this.ĕ = ê;
}

void ë(Vector3 ç, Vector3 Ï, Vector3 ê, bool é) {
    ë(ç, Ï, ê, 2f, é);
}

void è(Vector3 ç, float æ, bool é) {
    Ė = æ;
    this.Ě = true;
    this.Ę = é;
    ę = true;
    ě = false;
    this.ç = ç;
}

void å() {
    ñ(false, false, false, ĉ, 0);
    Č = false;
}

void ñ(Vector3 ý, float ú) {
    ñ(true, false, false, ý, ú);
}

void ñ(bool Ā, bool ÿ, bool þ, Vector3 ý, float ú) {
    ñ(Ā, ÿ, þ, ý, ý - ɉ, 0.0f, ú);
}

void ñ(bool Ā, bool ÿ, bool þ, Vector3 ý, Vector3 ü, float û, float ú) {
    Č = true;
    this.Ď = Ā;
    ĉ = ý;
    this.Ċ = ú;
    this.û = û;
    this.ċ = ÿ;
    this.č = þ;
    this.ü = ü;
    Ԍ = Vector3.Distance(ý, ɉ);
}

bool ù() {
    Vector3D ø;
    return this.Э.TryGetPlanetPosition(out ø);
}

Dictionary<String, float[]> ö = new Dictionary<string, float[]>();
float õ;

void ô() {
    if (!G_VAR1)
        return;

    try {
        õ = Runtime.CurrentInstructionCount;
    }catch {

    }
}

void ó(String Ô) {
    if (!G_VAR1)
        return;

    if (õ == 0)
        return;

    try {
        float Ɩ = (Runtime.CurrentInstructionCount - õ) / Runtime.MaxInstructionCount * 100;
        if (!ö.ContainsKey(Ô))
            ö.Add(Ô, new float[]{Ɩ,Ɩ});
        else {
            ö[Ô][0] = Ɩ;
            ö[Ô][1] = Math.Max(Ɩ, ö[Ô][1]);
        }
    } catch {

    }
}

string ǅ(float â) {
    return Math.Round(â, 2) + " ";
}

string ǅ(Vector3 â) {
    return "X" + ǅ(â.X) + "Y" + ǅ(â.Y) + "Z" + ǅ(â.Z);
}

Exception ȥ = null;
void Ȥ() {
    String O = "Error occurred! \nPlease copy this and paste it \nin the \"Bugs and issues\" discussion.\n" + "Version: " + VERSION + "\n";
    Ţ(П, setLCDFontAndSize, 0.9f, false);
    Ţ(О, setLCDFontAndSize, 0.9f, true);
    for (int A = 0; A < П.Count; A++)
        П[A].WriteText(O + ȥ.ToString());

    for (int A = 0; A < О.Count; A++)
        О[A].WriteText(O + ȥ.ToString());
}

const String ȣ = "INSTRUCTIONS";
const String Ȣ = "DEBUG";
String Ƕ = "", ȡ = "";
String Ǹ = "";
void ȟ() {
    String Ȟ = "";
    String ǒ = "";
    Ǹ = ϣ(false);
    Ȟ += Ǹ;
    ǒ += Ǹ;
    ǒ += γ();
    for (int A = 0; A < П.Count; A++)
        П[A].WriteText(Ȟ);

    for (int A = 0; A < О.Count; A++)
        О[A].WriteText(Ȟ);

    Echo(ǒ);
    for (int A = 0; A < Н.Count; A++) {
        IMyTextPanel ƨ = Н[A];
        String Ǩ = ƨ.CustomData.ToUpper();
        if (Ǩ == Ȣ)
            ƨ.WriteText(Ƕ + "\n" + ȡ);

        if (Ǩ == ȣ)
            ƨ.WriteText(Ƞ());
    }
}

string Ƞ() {
    String O = "";
    try {
        float Ʊ = Runtime.MaxInstructionCount;
        O += "Inst: " + Runtime.CurrentInstructionCount + " Time: " + Math.Round(Runtime.LastRunTimeMs, 3) + "\n";
        O += "Inst. avg/max: " + (int)(Χ * Ʊ) + " / " + (int)(Ψ * Ʊ) + "\n";
        O += "Inst. avg/max: " + Math.Round(Χ * 100f, 1) + "% / " + Math.Round(Ψ * 100f, 1) + "% \n";
        O += "Time avg/max: " + Math.Round(Υ, 2) + "ms / " + Math.Round(Φ, 2) + "ms \n";
    } catch {

    }

    for (int A = 0; A < ö.Count; A++) {
        O += "" + ö.Keys.ElementAt(A) + " = " + Math.Round(ö.Values.ElementAt(A)[0], 2) + " / " + Math.Round(ö.Values.ElementAt(A)[1], 2) + "%\n";
    }

    return O;
}

Vector3 ȴ(Vector3 ø, int ȳ) {
    return new Vector3(Math.Round(ø.X, ȳ), Math.Round(ø.Y, ȳ), Math.Round(ø.Z, ȳ));
}

Vector3 Ȳ(Vector3 ø, float ȱ) {
    Vector3 J = new Vector3(Math.Sign(ø.X), Math.Sign(ø.Y), Math.Sign(ø.Z));
    J.X = J.X == 0.0 ? ȱ : J.X; J.Y = J.Y == 0.0 ? ȱ : J.Y; J.Z = J.Z == 0.0 ? ȱ : J.Z;
    return J;
}

float Ȱ(Vector3 ȯ, Vector3 Ȫ) {
    if (ȯ == Ȫ)
        return 0;

    float ğ = (ȯ * Ȫ).Sum;
    float Ȯ = ȯ.Length();
    float ȭ = Ȫ.Length();
    if (Ȯ == 0 || ȭ == 0)
        return 0;

    float J = (float)((180.0f / Math.PI) * Math.Acos(ğ / (Ȯ * ȭ)));
    if (float.IsNaN(J))
        return 0;

    return J;
}

float Ȭ(Vector3 ȫ, Vector3 Ȫ, float ȩ) {
    float J = Ȱ(ȫ, Ȫ);
    if (ȩ > 0f)
        J *= -1;

    if (J > -90f)
        return J - 90f;
    else
        return 180f - (-J - 90f);
}

double Ȩ(float ȧ) {
    return (Math.PI / 180) * ȧ;
}

bool Ȧ(double ȵ, double ª, double ĝ) {
    return (ª >= ȵ && ª <= ĝ);
}

Vector3 Ȕ(IMyTerminalBlock q, Vector3 ȓ) {
    return Vector3D.Transform(ȓ, q.WorldMatrix);
}

Vector3 Ȓ(IMyTerminalBlock q, Vector3 ȑ) {
    return Ȑ(q, ȑ - q.GetPosition());
}

Vector3 Ȑ(IMyTerminalBlock q, Vector3 ȏ) {
    return Vector3D.TransformNormal(ȏ, MatrixD.Transpose(q.WorldMatrix));
}

Vector3 ȕ(Vector3 ȍ, Vector3 Ȍ, Vector3 ȏ) {
    MatrixD ȋ = MatrixD.CreateFromDir(ȍ, Ȍ);
    return Vector3D.TransformNormal(ȏ, MatrixD.Transpose(ȋ));
}

Vector3 Ȏ(Vector3 ȍ, Vector3 Ȍ, Vector3 đ) {
    MatrixD ȋ = MatrixD.CreateFromDir(ȍ, Ȍ);
    return Vector3D.Transform(đ, ȋ);
}

String Ȋ(Vector3 ø) {
    return "" + ø.X + "|" + ø.Y + "|" + ø.Z;
}

Vector3 ȉ(String O) {
    String[] ȝ = O.Split('|');
    return new Vector3(float.Parse(Ȗ(ȝ, 0)), float.Parse(Ȗ(ȝ, 1)), float.Parse(Ȗ(ȝ, 2)));
}

String Ȝ(Class1 B) {
    String ț = ":";
    String J = Ȋ(B.ɉ) + ț + Ȋ(B.Ï) + ț + Ȋ(B.ç) + ț + Ȋ(B.ĕ) + ț + Ȋ(B.Í);
    for (int A = 0; A < B.Θ.Length; A++) {
        J += ț;
        J += Math.Round(B.Θ[A], 3);
    }

    return J;
}

Class1 Ț(String ș) {
    String[] O = ș.Split(':');
    Class1 J = new Class1(ȉ(Ȗ(O, 0)), ȉ(Ȗ(O, 1)), ȉ(Ȗ(O, 2)), ȉ(Ȗ(O, 3)), ȉ(Ȗ(O, 4)));
    int A = 5;
    List<float> Ī = new List<float>();
    while (A < O.Length) {
        String Ș = Ȗ(O, A);
        float â = 0;
        if (!float.TryParse(Ș, out â))
            break;

        Ī.Add(â);
        A++;
    }

    J.Θ = Ī.ToArray();
    return J;
}

void ȗ<ŕ>(ŕ O, bool Ư) {
    if (Ư)
        Storage += "\n";

    Storage += O;
}

void ȗ<ŕ>(ŕ O) {
    ȗ(O, true);
}

String Ȗ(String[] O, int A) {
    String Ɨ = O.ElementAtOrDefault(A);
    if (String.IsNullOrEmpty(Ɨ))
        return "";

    return Ɨ;
}

bool ɜ = false; void Save() {
    if (ɜ || ƽ == Enum13.А) {
        Storage = "";
        return;
    }

    Storage = DATAREV + ";";
    ȗ(Ȋ(φ), false);
    ȗ(Ȋ(G_VAR43_Class1.Ï));
    ȗ(Ȋ(G_VAR43_Class1.ĕ));
    ȗ(Ȋ(G_VAR43_Class1.ç));
    ȗ(Ȋ(G_VAR43_Class1.Í));
    ȗ(Ȋ(G_VAR43_Class1.ɉ));
    ȗ(Ȋ(G_VAR43_Class1.Ή));
    ȗ(G_VAR43_Class1.Ͷ);
    ȗ(Ȋ(G_VAR58_Class1.ɉ));
    ȗ(Ȋ(G_VAR58_Class1.Í));
    ȗ(Ȋ(G_VAR58_Class1.Ï));
    ȗ(Ȋ(G_VAR58_Class1.ç));
    ȗ(Ȋ(G_VAR58_Class1.ĕ));
    ȗ(Ȋ(G_VAR58_Class1.Ή));
    ȗ(G_VAR58_Class1.Ͷ);
    ȗ(Ȋ(G_VAR59_Vector3));
    ȗ(Ȋ(G_VAR60_Vector3));
    ȗ(";");
    ȗ((int)ƽ, false);
    ȗ((int)G_VAR61_Enum9);
    ȗ((int)G_VAR69_Enum9);
    ȗ(G_VAR34_float);
    ȗ(G_VAR35_float);
    ȗ(G_VAR36_float);
    ȗ(G_VAR37_float);
    ȗ(G_VAR38_float);
    ȗ(G_VAR28_bool);
    ȗ(G_VAR33_bool);
    if (ƽ == Enum13.Ͼ) {
        ȗ((int)G_VAR19_Class5.Ų);
        ȗ(G_VAR19_Class5.Ƅ);
        ȗ(G_VAR19_Class5.ƅ);
        ȗ(G_VAR19_Class5.Ɣ);
        ȗ(G_VAR19_Class5.ƕ);
        ȗ((int)G_VAR20_Class5.Ų);
        ȗ(G_VAR20_Class5.Ƅ);
        ȗ(G_VAR20_Class5.ƅ);
        ȗ(G_VAR20_Class5.Ɣ);
        ȗ(G_VAR20_Class5.ƕ);
    } else {
        ȗ((int)G_VAR23_Enum4);
        ȗ((int)G_VAR21_Enum5);
        ȗ((int)G_VAR32_Enum6);
        ȗ((int)G_VAR27_Enum3);
        ȗ((int)G_VAR62_Enum4);
        ȗ(G_VAR22_bool);
        ȗ(G_VAR31_bool);
        ȗ(G_VAR29_bool);
        ȗ(G_VAR30_bool);
        ȗ(G_VAR24_int);
        ȗ(G_VAR25_int);
        ȗ(G_VAR26_int);
        ȗ(G_VAR39_float);
        ȗ(G_VAR40_float);
        ȗ(G_VAR41_float);
        ȗ(G_VAR42_float);
        ȗ(G_VAR63_int);
        ȗ(G_VAR64_int);
        ȗ(ԓ);
        ȗ(Ԓ);
        ȗ(Ԑ);
        ȗ(ԑ);
        ȗ(Ҍ);
        ȗ(Ԁ);
    }

    ȗ(";");
    for (int A = 0; A < G_VAR50_List_String.Count; A++)
        ȗ((A > 0 ? "|" : "") + G_VAR50_List_String[A], false);

    ȗ(";");
    for (int A = 0; A < G_VAR51_List_Class1.Count; A++)
        ȗ(Ȝ(G_VAR51_List_Class1[A]), A > 0);
}

Enum9 ɛ = Enum9.Ӌ;

/**
 * Original: ɚ
 */
public enum Enum16 {
    ə, ɘ, ɗ, Ə
}

Enum16 ɖ() {
    if (Storage == "")
        return Enum16.ɗ;

    String[] ɕ = Storage.Split(';');
    if (Ȗ(ɕ, 0) != DATAREV) {
        return Enum16.Ə;
    }

    int A = 0;
    try {
        String[] O = Ȗ(ɕ, 1).Split('\n');
        φ = ȉ(Ȗ(O, A++));
        G_VAR43_Class1.Ï = ȉ(Ȗ(O, A++));
        G_VAR43_Class1.ĕ = ȉ(Ȗ(O, A++));
        G_VAR43_Class1.ç = ȉ(Ȗ(O, A++));
        G_VAR43_Class1.Í = ȉ(Ȗ(O, A++));
        G_VAR43_Class1.ɉ = ȉ(Ȗ(O, A++));
        G_VAR43_Class1.Ή = ȉ(Ȗ(O, A++));
        G_VAR43_Class1.Ͷ = bool.Parse(Ȗ(O, A++));
        G_VAR58_Class1.ɉ = ȉ(Ȗ(O, A++));
        G_VAR58_Class1.Í = ȉ(Ȗ(O, A++));
        G_VAR58_Class1.Ï = ȉ(Ȗ(O, A++));
        G_VAR58_Class1.ç = ȉ(Ȗ(O, A++));
        G_VAR58_Class1.ĕ = ȉ(Ȗ(O, A++));
        G_VAR58_Class1.Ή = ȉ(Ȗ(O, A++));
        G_VAR58_Class1.Ͷ = bool.Parse(Ȗ(O, A++));
        G_VAR59_Vector3 = ȉ(Ȗ(O, A++));
        G_VAR60_Vector3 = ȉ(Ȗ(O, A++));
        O = Ȗ(ɕ, 2).Split('\n');
        A = 0;
        ƽ = (Enum13)int.Parse(Ȗ(O, A++));
        G_VAR61_Enum9 = (Enum9)int.Parse(Ȗ(O, A++));
        G_VAR69_Enum9 = (Enum9)int.Parse(Ȗ(O, A++));
        G_VAR34_float = int.Parse(Ȗ(O, A++));
        G_VAR35_float = int.Parse(Ȗ(O, A++));
        G_VAR36_float = int.Parse(Ȗ(O, A++));
        G_VAR37_float = int.Parse(Ȗ(O, A++));
        G_VAR38_float = float.Parse(Ȗ(O, A++));
        G_VAR28_bool = bool.Parse(Ȗ(O, A++));
        G_VAR33_bool = bool.Parse(Ȗ(O, A++));
        if (ƽ == Enum13.Ͼ) {
            G_VAR19_Class5.Ų = (Enum15)int.Parse(Ȗ(O, A++));
            G_VAR19_Class5.Ƅ = float.Parse(Ȗ(O, A++));
            G_VAR19_Class5.ƅ = float.Parse(Ȗ(O, A++));
            G_VAR19_Class5.Ɣ = Ȗ(O, A++);
            G_VAR19_Class5.ƕ = Ȗ(O, A++);
            G_VAR20_Class5.Ų = (Enum15)int.Parse(Ȗ(O, A++));
            G_VAR20_Class5.Ƅ = float.Parse(Ȗ(O, A++));
            G_VAR20_Class5.ƅ = float.Parse(Ȗ(O, A++));
            G_VAR20_Class5.Ɣ = Ȗ(O, A++);
            G_VAR20_Class5.ƕ = Ȗ(O, A++);
        } else {
            G_VAR23_Enum4 = (Enum4)int.Parse(Ȗ(O, A++));
            G_VAR21_Enum5 = (Enum5)int.Parse(Ȗ(O, A++));
            G_VAR32_Enum6 = (Enum6)int.Parse(Ȗ(O, A++));
            G_VAR27_Enum3 = (Enum3)int.Parse(Ȗ(O, A++));
            G_VAR62_Enum4 = (Enum4)int.Parse(Ȗ(O, A++));
            G_VAR22_bool = bool.Parse(Ȗ(O, A++));
            G_VAR31_bool = bool.Parse(Ȗ(O, A++));
            G_VAR29_bool = bool.Parse(Ȗ(O, A++));
            G_VAR30_bool = bool.Parse(Ȗ(O, A++));
            G_VAR24_int = int.Parse(Ȗ(O, A++));
            G_VAR25_int = int.Parse(Ȗ(O, A++));
            G_VAR26_int = int.Parse(Ȗ(O, A++));
            G_VAR39_float = float.Parse(Ȗ(O, A++));
            G_VAR40_float = float.Parse(Ȗ(O, A++));
            G_VAR41_float = float.Parse(Ȗ(O, A++));
            G_VAR42_float = float.Parse(Ȗ(O, A++));
            G_VAR63_int = int.Parse(Ȗ(O, A++));
            G_VAR64_int = int.Parse(Ȗ(O, A++));
            ԓ = int.Parse(Ȗ(O, A++));
            Ԓ = int.Parse(Ȗ(O, A++));
            Ԑ = int.Parse(Ȗ(O, A++));
            ԑ = int.Parse(Ȗ(O, A++));
            Ҍ = int.Parse(Ȗ(O, A++));
            Ԁ = float.Parse(Ȗ(O, A++));
        }

        O = Ȗ(ɕ, 3).Replace("\n", "").Split('|');
        G_VAR50_List_String = O.ToList();
        O = Ȗ(ɕ, 4).Split('\n');
        G_VAR51_List_Class1.Clear();
        if (O.Count() >= 1 && O[0] != "")
            for (int ã = 0; ã < O.Length; ã++)
                G_VAR51_List_Class1.Add(Ț(Ȗ(O, ã)));
    }
    catch {
        return Enum16.ɘ;
    }

    ɛ = G_VAR69_Enum9;
    Ҕ();
    return Enum16.ə;
}

String ɔ(String Ǡ) {
    int A = Ǡ.IndexOf("//");
    if (A != -1)
        Ǡ = Ǡ.Substring(0, A);

    String[] O = Ǡ.Split('=');
    if (O.Length <= 1)
        return "";

    return O[1].Trim();
}

String ɓ(String[] O, String ǩ, ref bool Ã) {
    foreach (String ū in O)
        if (ū.StartsWith(ǩ))
            return ū;

    Ã = false;
    return "";
}

bool ɯ = false;
bool ɰ = false;
String ɮ = "";
String ɭ = "";
IMyBroadcastListener ɬ = null;
bool ɫ = true;
void ɪ() {
    bool ɩ = true;
    if (ɫ) {
        ɫ = false;
        if (Me.CustomData.Contains("Antenna_Name")) {
            G_VAR12 = "Update custom data";
            Me.CustomData = "";
        }
    }

    String ɨ = (ƽ != Enum13.А ? "[PAM-Ship]" : "[PAM-Controller]") + " Broadcast-settings";
    try {
        if (Me.CustomData.Length == 0 || Me.CustomData.Split('\n')[0] != ɨ)
            ɣ(ɨ);

        String[] O = Me.CustomData.Split('\n');
        ɯ = bool.Parse(ɔ(ɓ(O, "Enable_Broadcast", ref ɩ)));
        bool ɧ = false;
        bool ɦ = true;
        if (ɯ) {
            if (ƽ != Enum13.А) {
                ɮ = ɔ(ɓ(O, "Ship_Name", ref ɩ)).Replace(ɟ, "");
            }

            ɭ = ɔ(ɓ(O, "Broadcast_Channel", ref ɩ)).ToLower();
            ɦ =false;
            if (ɬ == null) {
                ɬ = this.IGC.RegisterBroadcastListener(ɠ);
                ɬ.SetMessageCallback("");
            }

            List<IMyRadioAntenna> ɥ = new List<IMyRadioAntenna>();
            GTS.GetBlocksOfType(ɥ);
            bool ɤ = false;
            for (int A = 0; A < ɥ.Count; A++) {
                if (ɥ[A].EnableBroadcasting && ɥ[A].Enabled) {
                    ɤ = true;
                    break;
                }
            }

            if (ɥ.Count == 0)
                G_VAR12 = "No Antenna found";
            else if (!ɤ)
                G_VAR12 = "Antenna not ready";

            ɧ = ɥ.Count == 0 || !ɤ;
            if (ɰ && !ɧ && ƽ != Enum13.А)
                G_VAR12 = "Antenna ok";
        }
        else if (ƽ == Enum13.А)
            G_VAR12 = "Offline - Enable in PB custom data";

        ɰ = ɧ;
        if (ɦ) {
            if (ɬ != null)
                this.IGC.DisableBroadcastListener(ɬ);

            ɬ = null;
        }
    } catch {
        ɩ = false;
    }

    if (!ɩ) {
        G_VAR12 = "Reset custom data";
        ɣ(ɨ);
    }
}

void ɣ(String ɢ) {
    String Ǩ = ɢ + "\n\n" + "Enable_Broadcast=" + (ƽ == Enum13.А ? "true" : "false") + " \n";
    Ǩ += ƽ != Enum13.А ? "Ship_Name=Your_ship_name_here\n" : "";
    Me.CustomData = Ǩ + "Broadcast_Channel=#default";
}

String ɡ() {
    if (ƽ != Enum13.А)
        return "" + Me.GetId();

    return ɟ;
}

const String ɠ = "[PAMCMD]";
const String ɟ = "#";
const Char ɞ = ';';

void ɝ(String ƿ, String ȷ) {
    ɀ(ƿ, ɟ, ȷ);
}

void ɀ(String ƿ, String ý, String ȷ) {
    try {
        if (!ɯ)
            return;

        ƿ = ɠ + ɞ + ɡ() + ɞ + ý + ɞ + ɭ + ɞ + ȷ + ɞ + ƿ;
        this.IGC.SendBroadcastMessage(ɠ, ƿ);
    } catch (Exception e) {
        ȥ = e;
    }
}

bool Ⱦ(String ƿ) {
    return ƿ.StartsWith(ɠ);
}

bool Ƚ(ref String ū, out string Ƹ, Char ȼ) {
    int A = ū.IndexOf(ȼ);
    Ƹ = "";
    if (A < 0)
        return false;

    Ƹ = ū.Substring(0, A);
    ū = ū.Remove(0, A + 1);
    return true;
}

String ȿ = "";
bool Ȼ(ref String ƿ, out String ǋ, out String ȹ) {
    ǋ = "";
    ȹ = "";
    if (!ɯ)
        return false;

    String O = "";
    if (!Ƚ(ref ƿ, out O, ɞ) || !Ⱦ(O))
        return false;

    if (!Ƚ(ref ƿ, out ǋ, ɞ))
        return false;

    if (!Ƚ(ref ƿ, out O, ɞ) || (O != ɡ() && (O != "*" && ƽ != Enum13.А)))
        return false;

    if (!Ƚ(ref ƿ, out O, ɞ) || (O != ɭ))
        return false;

    if (!Ƚ(ref ƿ, out ȹ, ɞ))
        return false;

    return true;
}

void ȸ(String ȷ) {
    if (!ɯ)
        return;

    String ȶ = "" + ɞ;
    String ƿ = ƿ = VERSION + ȶ;
    ƿ += ɮ + ȶ;
    ƿ += (int)ƽ + ȶ;
    ƿ += ǅ(ɉ.X) + "" + ȶ;
    ƿ += ǅ(ɉ.Y) + ȶ;
    ƿ += ǅ(ɉ.Z) + ȶ;
    if (ƽ != Enum13.Ͼ)
        ƿ += Ϝ(G_VAR69_Enum9) + (G_VAR69_Enum9 == Enum9.Ӊ ? " " + Math.Round(G_VAR65_double, 1) + "%" : "") + ȶ;
    else
        ƿ += Ϝ(G_VAR69_Enum9) + ȶ;

    ƿ += G_VAR12 + ȶ;
    ƿ += G_VAR6 + "" + ȶ;
    ƿ += Ǹ + ȶ;
    ƿ += œ + ȶ;
    ƿ += ł + ȶ;
    for (int A = 0; A < ŀ.Count; A++) {
        if (ŀ[A].ĭ != Enum14.ň)
            continue;

        ƿ += ŀ[A].Ô + "/" + ŀ[A].ń + "/" + ŀ[A].Ń + ȶ;
    }

    ɝ(ƿ, ȷ);
}

/**
 * Original: Ⱥ
 */
public enum Enum17 {
    ǻ, Ɂ, ɒ, ɑ
}

/**
 * Original: ɐ
 */ 
class Class6 {
    public DateTime ɏ;
    public DateTime Ɏ;
    public String ǃ = "";
    public String Ô = "";
    public String ɍ = "";
    public String Ɍ = "";
    public String ɋ = "";
    public String Ǹ = "";
    public String Ɋ = "";
    public Vector3 ɉ = new Vector3();
    public List<Class4> Ƿ = new List<Class4>();
    public Enum13 ƽ = Enum13.ǻ;
    public Enum17 Ɉ;
    public float ɇ;
    public float Ɇ;
    public int Ʋ;
    public int Ʌ = 0;
    public int Ʉ = 0;

    public bool Ƀ() {
        return (DateTime.Now - ɏ).TotalSeconds > 10;
    }

    public bool ɂ() {
        if (Ɉ != Enum17.Ɂ)
            return false;

        return (DateTime.Now - Ɏ).TotalSeconds >= 4;
    }

    public Class6(String ǃ) {
        this.ǃ = ǃ;
    }

    public Enum17 ǫ(String ǃ, bool ő, bool ǂ, bool ǁ) {
        if (ǃ == "" && !ǂ)
            return Enum17.ǻ;

        if (Ɉ == Enum17.Ɂ && ɂ())
            Ɉ = Enum17.ɑ;

        if (ő) {
            Ɋ = ǃ;
            Ɉ = Enum17.Ɂ; Ɏ = DateTime.Now; return Ɉ;
        } else if (ǂ) {
            Ɋ = ""; Ɉ = Enum17.ǻ;
        } else if (Ɋ == ǃ) {
            if (ǁ)
                Ɉ = Enum17.ɒ;

            return Ɉ;
        }

        return Enum17.ǻ;
    }
}

void ǀ(Class6 Ʀ, String ƿ) {
    Ʀ.ɏ = DateTime.Now;
    String Ä = "";
    String ƾ = "";
    String Ǆ = "";
    String ƽ = "";
    String[] ø = new string[3];
    Ƚ(ref ƿ, out Ʀ.ɍ, ɞ);
    Ƚ(ref ƿ, out Ʀ.Ô, ɞ);
    if (Ʀ.ɍ != VERSION)
        return;

    Ƚ(ref ƿ, out ƽ, ɞ);
    Ƚ(ref ƿ, out ø[0], ɞ);
    Ƚ(ref ƿ, out ø[1], ɞ);
    Ƚ(ref ƿ, out ø[2], ɞ);
    Ƚ(ref ƿ, out Ʀ.ɋ, ɞ);
    Ƚ(ref ƿ, out Ʀ.Ɍ, ɞ);
    Ƚ(ref ƿ, out Ä, ɞ);
    Ƚ(ref ƿ, out Ʀ.Ǹ, ɞ);
    Ƚ(ref ƿ, out ƾ, ɞ);
    Ƚ(ref ƿ, out Ǆ, ɞ);
    Ʀ.Ƿ.Clear();
    while (true) {
        String A;
        if (!Ƚ(ref ƿ, out A, ɞ))
            break;

        String[] O = A.Split('/');
        if (O.Count() < 3)
            continue;

        int ƻ = 0;
        if (!int.TryParse(O[2], out ƻ))
            continue;

        Ʀ.Ƿ.Add(new Class4(O[0], O[1], ƻ, Enum14.ň));
    }

    int ƺ = 0;
    if (!int.TryParse(Ä, out Ʀ.Ʋ))
        Ʀ.Ʋ = 0;

    if (!int.TryParse(ƽ, out ƺ))
        Ʀ.ƽ = Enum13.ǻ;

    if (!float.TryParse(ø[0], out Ʀ.ɉ.X))
        Ʀ.ɉ.X = 0;

    if (!float.TryParse(ø[1], out Ʀ.ɉ.Y))
        Ʀ.ɉ.Y = 0;

    if (!float.TryParse(ø[2], out Ʀ.ɉ.Z))
        Ʀ.ɉ.Z = 0;

    if (!float.TryParse(ƾ, out Ʀ.Ɇ))
        Ʀ.Ɇ = 0;

    if (!float.TryParse(Ǆ, out Ʀ.ɇ))
        Ʀ.ɇ = 0;

    Ʀ.ƽ = (Enum13)ƺ;
    Ʀ.Ʌ = (int)Math.Round(Vector3.Distance(Me.GetPosition(), Ʀ.ɉ));
    Ʀ.Ʉ = 0;
    for (int ã = 0; ã < Ʀ.Ƿ.Count(); ã++)
        Ʀ.Ʉ += Ʀ.Ƿ[ã].Ń;
}

void ƹ(string Ƹ) {
    if (Ƹ == "")
        return;

    var ª = Ƹ.ToUpper().Split(' ');
    ª.DefaultIfEmpty("");
    var Ƽ = ª.ElementAtOrDefault(0);
    var Ʒ = ª.ElementAtOrDefault(1);
    String ǆ = "Invalid argument: " + Ƹ;
    if (G_VAR7 == Enum1.Ϭ) {
        if (ƞ != null) {
            switch (Ƽ) {
                case "UP":
                    {
                        if (G_VAR6 < 2)
                            break;

                        if (ƞ.Ʋ == 0) {
                            G_VAR6 = 1;
                            return;
                        }

                        Ƨ(ƞ, "UP");
                        return;
                    }

                case "DOWN":
                    {
                        if (G_VAR6 < 1)
                            break;
                        if (G_VAR6 == 1) {
                            Ƨ(ƞ, "MRES");
                            break;
                        }

                        Ƨ(ƞ, "DOWN");
                        break;
                    }

                case "APPLY":
                    {
                        if (G_VAR6 < 2)
                            break;

                        Ƨ(ƞ, "APPLY");
                        return;
                    }
            }
        }
    }

    switch (Ƽ) {
        case "UP":
            this.Ј(false);
            return;

        case "DOWN":
            {
                this.Ї(false);
                return;
            }

        case "APPLY":
            this.Ɯ(true);
            return;
    }

    switch (Ƽ) {
        case "CLEAR":
            Ơ();
            return;

        case "SENDALL":
            Ƨ(null, Ʒ);
            return;

        case "SEND":
            ǐ(Ƹ.Remove(0, "SEND".Length + 1));
            return;
    }

    G_VAR12 = ǆ;
}

void ǐ(String Ƹ) {
    if (Ƹ == "")
        return;

    var ª = Ƹ.Split(':');
    if (ª.Length != 2) {
        G_VAR12 = "Missing separator \":\"";
        return;
    };

    ª.DefaultIfEmpty("");
    String Ǐ = ª.ElementAtOrDefault(0).Trim();
    Class6 Ʀ = ǎ("", Ǐ);
    if (Ʀ != null)
        Ƨ(Ʀ, ª.ElementAtOrDefault(1).Trim());
    else
        G_VAR12 = "Unknown ship: " + Ǐ;
}

Class6 ǎ(String ǃ, String Ô) {
    ǃ = ǃ.ToUpper(); Ô = Ô.ToUpper();
    for (int A = 0; A < Ǎ.Count; A++) {
        if (ǃ != "" && Ǎ[A].ǃ.ToUpper() == ǃ)
            return Ǎ[A];

        if (Ô != "" && Ǎ[A].Ô.ToUpper() == Ô)
            return Ǎ[A];
    }

    return null;
}

List<Class6> Ǎ = null; void ǌ(string Ƹ) {
    if (ρ) {
        Ǎ = new List<Class6>();
    }

    String ǋ = "";
    String Ǌ = "";
    if (!Ⱦ(Ƹ))
        ƹ(Ƹ);

    if (ɬ != null && ɬ.HasPendingMessage) {
        MyIGCMessage ǉ = ɬ.AcceptMessage();
        String ž = (string)ǉ.Data;
        if (Ȼ(ref ž, out ǋ, out Ǌ) && ǋ != "" && ǋ != ɟ) {
            Class6 Ʀ = ǎ(ǋ, "");
            if (Ʀ == null) {
                Ʀ = new Class6(ǋ);
                Ǎ.Add(Ʀ);
            }

            Ʀ.ǫ(Ǌ, false, false, true);
            ǀ(Ʀ, ž);
            Ǎ.Sort(delegate (Class6 Y, Class6 X) {
                if (Y.Ô == null && X.Ô == null)
                    return 0;
                else if (Y.Ô == null)
                    return -1;
                else if (X.Ô == null)
                    return 1;
                else
                    return Y.Ô.CompareTo(X.Ô);
            });
        }
    }

    if (ο || ρ) {
        if (Ϊ <= 0 || ρ) {
            G_VAR12 = "";
            ρ = false;
            Ƥ();
            ɪ();
            ς = DateTime.Now;
        }
    }

    Ǘ();
}

Enum17 ǈ(Class6 Ʀ, bool Ǉ, string ǃ) {
    Enum17 J = Enum17.ǻ;
    if (Ʀ == null) {
        for (int A = 0; A < Ǎ.Count; A++) {
            Ʀ = Ǎ[A];
            Enum17 Ƶ = Ʀ.ǫ(ǃ, false, Ǉ, false);
            if (Ƶ == Enum17.ɒ)
                J = Ƶ;

            if (Ƶ == Enum17.ɑ)
                return Ƶ;

            if (Ƶ == Enum17.Ɂ)
                return Ƶ;
        }

        return J;
    } else
        return Ʀ.ǫ(ǃ, false, Ǉ, false);
}

void Ƨ(Class6 Ʀ, String ƥ) {
    if (Ʀ == null) {
        for (int A = 0; A < Ǎ.Count; A++)
            Ǎ[A].ǫ(ƥ, true, false, false);

        ɀ(ƥ, "*", ƥ);
    } else {
        Ʀ.ǫ(ƥ, true, false, false);
        ɀ(ƥ, Ʀ.ǃ, ƥ);
    }
}

void Ƥ() {
    List<IMyTerminalBlock> ƣ = new List<IMyTerminalBlock>();
    GTS.GetBlocksOfType(П, Л);
    GTS.SearchBlocksOfName(pamTag.Substring(0, pamTag.Length - 1) + ":", ƣ, q => q.CubeGrid == Me.CubeGrid && q is IMyTextSurfaceProvider);
    П = Ѧ(П, pamTag, true);
    ѵ(ƣ);
    э();
    Ţ(П, setLCDFontAndSize, 1.15f, false);
    Ţ(О, setLCDFontAndSize, 1.15f, true);
    String Ƣ = ǳ(false);
    String ơ = "//Custom Data is obsolete, please delete it.\n\n";
    foreach (IMyTerminalBlock ƨ in П) {
        if (ƨ.CustomData == "") {
            ƨ.CustomData = ǳ(true);
            continue;
        }

        if (!ƨ.CustomData.Contains(Ƣ)) {
            if (!ƨ.CustomData.Contains(ơ))
                ƨ.CustomData = ơ + ƨ.CustomData;
        }
    }
}

void Ơ() {
    Ǎ.Clear();
    ƞ = null;
    Љ(Enum1.Ϸ);
    G_VAR6 = 0;
}

Class6 ƞ = null;
String Ɯ(bool ƛ) {
    int Ɲ = 0;
    return Ɯ(ƛ, 0, ref Ɲ, false, 1);
}

String Ɯ(bool ƛ, int ƚ, ref int ƙ, bool Ƙ, int Ɵ) {
    String Ɨ = "";
    String Ʃ = "——————————————————\n";
    String ƶ = "--------------------------------------------\n";
    if (ƞ == null || G_VAR7 == Enum1.Ϸ || G_VAR7 == Enum1.ϻ)
        Ɨ += "[PAM]-Controller | " + Ǎ.Count + " Connected ships" + "\n";
    else
        Ɨ += Ʈ(ƞ) + "\n";

    Ɨ += Ʃ;
    int A = 0;
    int ƴ = G_VAR6;
    G_VAR5 = 0;
    if (G_VAR12 != "") {
        ύ(ref Ɨ, ƴ, A++, ƛ, G_VAR12);
    } else if (G_VAR7 == Enum1.Ϸ) {
        if (ƞ == null && Ǎ.Count >= 1)
            ƞ = Ǎ[0];

        String Ƴ = "";
        if (ύ(ref Ƴ, ƴ, A++, ƛ, " Send command to all"))
            Љ(Enum1.ϻ);

        int Ʋ = 0;
        for (int ã = 0; ã < Ǎ.Count; ã++) {
            if (Ƙ) Ƴ += "\n";
            Class6 Ʊ = Ǎ[ã];
            if (ƴ == A || ƴ == A + 1)
                ƞ = Ʊ;

            if (A == ƴ)
                Ʋ = Ƴ.Split('\n').Length - 1;

            if (ύ(ref Ƴ, ƴ, A++, ƛ, " " + Ʈ(Ʊ))) {
                ƞ = Ʊ;
                Љ(Enum1.Ϭ);
                ƞ.ǫ("", false, true, false);
            }

            if (A == ƴ)
                Ʋ = Ƴ.Split('\n').Length - 1;

            if (ύ(ref Ƴ, ƴ, A++, ƛ, " " + (Ƙ ? "" : "  ") + ƭ(Ʊ))) {
                ƞ = Ʊ;
                Љ(Enum1.Ƿ);
            }
        }

        int ư = Ɵ - 2;
        ư += Math.Max(0, (ƚ - 1)) * Ɵ;
        Ɨ += ǧ(ư, Ƴ, Ʋ, ref ƙ);
    } else if (G_VAR7 == Enum1.ž || G_VAR7 == Enum1.ϻ) {
        Enum1 J = G_VAR7 == Enum1.ž ? Enum1.Ϭ : Enum1.Ϸ;
        Class6 Ʀ = null;
        if (G_VAR7 == Enum1.ž)
            Ʀ = ƞ;

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Back")) {
            Љ(J);
        }

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " [Stop] " + Ǔ(Ʀ, "STOP"))) {
            Ƨ(Ʀ, "STOP");
        }

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " [Continue] " + Ǔ(Ʀ, "CONT"))) {
            Ƨ(Ʀ, "CONT");
        }

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " [Move home] " + Ǔ(Ʀ, "HOMEPOS"))) {
            Ƨ(Ʀ, "HOMEPOS");
        }

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " [Move to job] " + Ǔ(Ʀ, "JOBPOS"))) {
            Ƨ(Ʀ, "JOBPOS");
        }

        if (ύ(ref Ɨ, ƴ, A++, ƛ, " [Full simulation] " + Ǔ(Ʀ, "FULL"))) {
            Ƨ(Ʀ, "FULL");
        }

        if (Ʀ != null && Ʀ.ƽ == Enum13.Ͼ)
            if (ύ(ref Ɨ, ƴ, A++, ƛ, " [Undock] " + Ǔ(Ʀ, "UNDOCK"))) {
                Ƨ(Ʀ, "UNDOCK");
            }
    }
    else if (G_VAR7 == Enum1.Ϭ) {
        String Ư = "";
        if (ύ(ref Ư, ƴ, A++, ƛ, " Back")) {
            Љ(Enum1.Ϸ);
        };
        Ɨ += Ư.Substring(0, Ư.Length - 1).PadRight(36, ' ');
        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Send cmd")) {
            Љ(Enum1.ž);
        }

        if (!ƞ.Ƀ() && !ƞ.ɂ())
            G_VAR5++;

        if (ƞ.ɂ())
            Ɨ += "No answer received...";
        else
            Ɨ += ƪ(ƞ, ƞ.Ǹ);
    } else if (G_VAR7 == Enum1.Ƿ) {
        if (ύ(ref Ɨ, ƴ, A++, ƛ, " Back")) {
            Љ(Enum1.Ϸ);
        }

        Ɨ += ƶ;
        Ɨ += ƪ(ƞ, ǘ(ƞ));
    }

    if (!G_VAR4)
        G_VAR6 = Math.Min(G_VAR5 - 1, G_VAR6);

    return Ɨ;
}

String Ʈ(Class6 Ʀ) {
    if (Ʀ.ɍ != VERSION)
        return Ʀ.Ô + ": Different version!";

    return Ʀ.Ô + ": " + ƪ(Ʀ, Ʀ.ɋ) + " " + Ʀ.Ʌ + "m"; 
}

String ƭ(Class6 Ʀ) {
    String O = ǿ("", µ(Ʀ.Ɇ, Ʀ.ɇ) * 100f, 100f, 8, 0, 0) + "% ";
    for (int A = 0; A < Ʀ.Ƿ.Count; A++) {
        if (A >= 5) break;
        Class4 Ĵ = Ʀ.Ƿ[A];
        O += Ǜ(Ȁ(Ƭ(Ĵ)), 3) + " " + ǖ(Ĵ.Ń) + " ";
    }

    return O;
}

String Ƭ(Class4 Ĵ) {
    if (Ĵ.ń.ToUpper() == "ORE" || Ĵ.ń.ToUpper() == "INGOT") {
        String ƫ = GetElementCode(Ĵ.Ô.ToUpper());
        if (ƫ != "")
            return ƫ;
    }

    return Ĵ.Ô;
}

String ƪ(Class6 Ʀ, String O) {
    if (Ʀ.Ƀ())
        return "No signal...(" + Ǖ((int)(DateTime.Now - Ʀ.ɏ).TotalSeconds) + ")";

    return O;
}

/**
 * Original: Ǒ
 */
public enum Enum18 {
    ǻ, ǹ, Ǹ, Ƿ, Ƕ, ǵ, Ǵ
}

String ǳ(bool ǲ) {
    String J = "";
    if (ǲ) J += "mode=main:1\n\n";
    J += "//Available modes:\n" + "//main:<Page>\n" + "//mainX:<Page>  (no empty lines)\n" + "//menu\n" + "//inventory\n" + "//menu:<shipname>\n" + "//inventory:<shipname>";
    return J;
}

void Ǳ(IMyTerminalBlock ƨ, out Enum18 ǰ, out String ǩ) {
    bool Ɲ = true;
    String ƽ = ɔ(ɓ(ƨ.CustomData.Split('\n'), "mode", ref Ɲ)).ToUpper();
    String[] ǯ = ƽ.Split(':');
    String Ǯ = ǭ(ǯ, 0);
    ǩ = ǭ(ǯ, 1);
    ǰ = Enum18.ǻ;
    if (Ǯ == "MAIN")
        ǰ = Enum18.ǹ;
    else if (Ǯ == "MAINX")
        ǰ = Enum18.Ǵ;
    else if (Ǯ == "MENU")
        ǰ = Enum18.Ǹ;
    else if (Ǯ == "INVENTORY")
        ǰ = Enum18.Ƿ;
    else if (Ǯ == "DEBUG")
        ǰ = Enum18.Ƕ;
    if (ǰ == Enum18.ǻ)
        ǩ = ƽ;
}

String ǭ(String[] ª, int E) {
    if (E < ª.Length)
        return ª[E].Trim();

    return "";
}

int Ǭ = 0;
int Ǻ = 0;
int Ǽ = 0;
int ȇ = 0;
int Ȉ = 0;
int Ȇ = 0;
int ȅ = 0;
String Ȅ(Enum18 ƽ, String ǩ, bool Ç) {
    int ȃ = 15;
    if (Ç) {
        Ǻ = Ǭ;
        Ǭ = 0;
        ȇ = Ǽ;
        Ǽ = 0;
    }

    String J = "";
    if (ƽ == Enum18.ǹ) {
        int E = 0;
        J = Ɯ(false, Ǻ, ref Ȉ, true, ȃ);
        if (!int.TryParse(ǩ, out E))
            return J; Ǭ = Math.Max(E, Ǭ);

        E--;
        return Ǣ(E, ȃ, J);
    } else if (ƽ == Enum18.Ǵ) {
        int E = 0;
        J = Ɯ(false, ȇ, ref Ȇ, false, ȃ);
        if (!int.TryParse(ǩ, out E))
            return J;

        Ǽ = Math.Max(E, Ǽ);
        E--;
        return Ǣ(E, ȃ, J);
    }

    if (ƽ == Enum18.ǵ) {
        return Ɯ(false, 0, ref ȅ, true, ȃ);
    } else if (ƽ == Enum18.Ƿ || ƽ == Enum18.Ǹ || ƽ == Enum18.Ƕ) {
        Class6 Ʀ = ƞ;
        if (ǩ != "") {
            Ʀ = ǎ("", ǩ);
            if (Ʀ == null)
                return "Unknown ship: " + ǩ;
        } else {
            if (Ʀ == null)
                return "No ship on main screen selected.";
            J = "Selected: ";
        }

        String Ʃ = "—————————————————————\n";
        String Ȃ = "";
        String ȁ = "";
        if (ƽ == Enum18.Ǹ) {
            Ȃ = "Menu";
            ȁ = ƪ(Ʀ, Ʀ.Ǹ);
        } else if (ƽ == Enum18.Ƿ) {
            Ȃ = "Inventory";
            ȁ = ƪ(Ʀ, ǘ(Ʀ));
        } else if (ƽ == Enum18.Ƕ) {
            return ƪ(Ʀ, Ǚ(Ʀ));
        }

        J += Ʀ.Ô + " - " + Ʀ.Ʌ + "m | " + Ȁ("" + Ȃ) + "\n" + Ʃ;
        J += ȁ; return J;
    }

    return "Unknown command: " + ǩ; ;
}

String Ȁ(String O) {
    if (O == "")
        return O;

    return O.First().ToString().ToUpper() + O.Substring(1).ToLower();
}

string ǿ(String Ô, float Ń, float ĝ, int Ǿ, int ǽ, int Ǫ) {
    float ǜ = µ(Ń, ĝ) * Ǿ;
    String O = "[";
    for (int A = 0; A < Ǿ; A++) {
        if (A <= ǜ)
            O += "|";
        else
            O += "'";
    }

    O += "]";
    return O + " " + Ǜ(Ȁ(Ô), ǽ).PadRight(ǽ) + "".PadRight(Ǫ) + ǖ(Ń);
}

String Ǜ(String O, int ǚ) {
    if (O == "")
        return O;

    if (O.Length > ǚ)
        O = O.Substring(0, ǚ - 1) + ".";

    return O;
}

string Ǚ(Class6 Ʀ) {
    String O = "";
    O += Ʀ.ɍ + "\n";
    O += Ʀ.Ô + "\n";
    O += Ʀ.ɉ.X + "\n";
    O += Ʀ.ɉ.Y + "\n";
    O += Ʀ.ɉ.Z + "\n";
    O += Ʀ.ɋ + "\n";
    O += Ʀ.Ɍ + "\n";
    O += Ʀ.Ʋ + "\n";
    O += Ʀ.Ǹ.Length + "\n";
    O += Ʀ.Ɇ + "\n";
    O += Ʀ.ɇ + "\n";
    O += Ʀ.Ƿ.Count() + "\n";
    return O;
}

string ǘ(Class6 Ʀ) {
    String O = "";
    O += ǿ("All", µ(Ʀ.Ɇ, Ʀ.ɇ) * 100f, 100, 30, 8, 12) + "%\n";
    O += "\n";

    for (int ã = 0; ã < Ʀ.Ƿ.Count(); ã++) {
        Class4 Ĵ = Ʀ.Ƿ[ã];
        O += ǿ(Ĵ.Ô, Ĵ.Ń, Ʀ.Ʉ, 30, 8, 10) + "\n";
    }

    return O;
}

String ǖ(float Ń) {
    if (Ń >= 1000000)
        return Math.Round(Ń / 1000000f, Ń / 1000000f < 100 ? 1 : 0) + "M";

    if (Ń >= 1000)
        return Math.Round(Ń / 1000f, Ń / 1000f < 100 ? 1 : 0) + "K";

    return "" + Math.Round(Ń);
}

String Ǖ(int ǔ) {
    if (ǔ >= 60 * 60)
        return Math.Round(ǔ / (60f * 60f), 1) + " h";

    if (ǔ >= 60)
        return Math.Round(ǔ / 60f, 1) + " min";

    return "" + ǔ + " s";
}

String Ǔ(Class6 Ʀ, String ž) {
    Enum17 Ƶ = ǈ(Ʀ, false, ž);
    if (Ƶ == Enum17.ɒ)
        return "received!";

    if (Ƶ == Enum17.Ɂ)
        return "pending...";

    if (Ƶ == Enum17.ɑ)
        return "no answer!";

    return "";
}

void Ǘ() {
    String ǒ = "[PAM]-Controller\n\n" + 
        "Run-arguments: (Type without:[ ])\n" +
        "———————————————\n" +
        "[UP] Menu navigation up\n" +
        "[DOWN] Menu navigation down\n" +
        "[APPLY] Apply menu point\n" +
        "[CLEAR] Clear miner list\n" +
        "[SEND ship:cmd] Send to a ship*\n" +
        "[SENDALL cmd] Send to all ships*\n" +
        "———————————————\n\n" +
        "*[SEND] = Cmd to one ship:\n" +
        " e.g.: \"SEND Miner 1:homepos\"\n\n" +
        "*[SENDALL] = Cmd to all ships:\n" +
        " e.g.: \"SENDALL homepos\"\n\n";

    for (int A = 0; A < О.Count; A++)
        О[A].WriteText(Ȅ(Enum18.ǵ, "0", false));

    for (int A = 0; A < П.Count; A++) {
        Enum18 ƽ = Enum18.ǻ;
        String ǩ = "";
        Ǳ(П[A], out ƽ, out ǩ);
        П[A].WriteText(Ȅ(ƽ, ǩ, A == 0));
    }

    Echo(ǒ);
    for (int A = 0; A < Н.Count; A++) {
        IMyTextPanel ƨ = Н[A];
        String Ǩ = ƨ.CustomData.ToUpper();
        if (Ǩ == Ȣ)
            ƨ.WriteText(Ƕ + "\n" + ȡ);

        if (Ǩ == ȣ)
            ƨ.WriteText(Ƞ());
    }
}

String ǧ(int Ǧ, String ū, int ǥ, ref int Ǥ) {
    String[] ǟ = ū.Split('\n');
    if (ǥ >= Ǥ + Ǧ - 1)
        Ǥ++;

    Ǥ = Math.Min(ǟ.Count() - 1 - Ǧ, Ǥ);
    if (ǥ < Ǥ + 1)
        Ǥ--;

    Ǥ = Math.Max(0, Ǥ);
    String J = "";
    for (int A = 0; A < Ǧ; A++) {
        int ǣ = A + Ǥ;
        if (ǣ >= ǟ.Count())
            break;

        J += ǟ[ǣ] + "\n";
    }

    return J;
}

String Ǣ(int E, int ǡ, String Ǡ) {
    String[] ǟ = Ǡ.Split('\n');
    int G_VAR58_Class1 = E * ǡ;
    int ǝ = (E + 1) * (ǡ);

    String J = "";
    for (int A = G_VAR58_Class1; A < ǝ; A++) {
        if (A >= ǟ.Count())
            break;

        J += ǟ[A] + "\n";
    }

    return J;
}
