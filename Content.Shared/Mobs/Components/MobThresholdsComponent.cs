using Content.Shared.Alert;
using Content.Shared.FixedPoint;
using Content.Shared.Mobs.Systems;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared.Mobs.Components;

[RegisterComponent, NetworkedComponent]
[Access(typeof(MobThresholdSystem))]
public sealed partial class MobThresholdsComponent : Component
{
    [DataField(required: true)]
    public SortedDictionary<FixedPoint2, MobState> Thresholds = new();

    [DataField]
    public bool TriggersAlerts = true;

    [DataField]
    public MobState CurrentThresholdState;

    /// <summary>
    /// The health alert that should be displayed for player controlled entities.
    /// Used for alternate health alerts (silicons, for example)
    /// </summary>
    [DataField]
    public Dictionary<MobState, ProtoId<AlertPrototype>> StateAlertDict = new()
    {
        {MobState.Alive, "HumanHealth"},
        {MobState.Critical, "HumanCrit"},
        {MobState.SoftCritical, "HumanCrit"},
        {MobState.Dead, "HumanDead"},
    };

    [DataField]
    public ProtoId<AlertCategoryPrototype> HealthAlertCategory = "Health";

    /// <summary>
    /// Whether or not this entity should display damage overlays (robots don't feel pain, black out etc.)
    /// </summary>
    [DataField]
    public bool ShowOverlays = true;

    /// <summary>
    /// Whether or not this entity can be revived out of a dead state.
    /// </summary>
    [DataField]
    public bool AllowRevives;

    /// <summary>
    /// Track initial trashholds to modify thresholds in runtime depending on curent special value.
    /// </summary>
    [DataField("initialThresholds")]
    public SortedDictionary<FixedPoint2, MobState> BaseThresholds = new();

}

[Serializable, NetSerializable]
public sealed class MobThresholdsComponentState : ComponentState
{
    public Dictionary<FixedPoint2, MobState> UnsortedThresholds;

    public bool TriggersAlerts;

    public MobState CurrentThresholdState;

    public Dictionary<MobState, ProtoId<AlertPrototype>> StateAlertDict;

    public bool ShowOverlays;

    public bool AllowRevives;

    public MobThresholdsComponentState(Dictionary<FixedPoint2, MobState> unsortedThresholds,
        bool triggersAlerts,
        MobState currentThresholdState,
        Dictionary<MobState,
        ProtoId<AlertPrototype>> stateAlertDict,
        bool showOverlays,
        bool allowRevives)
    {
        UnsortedThresholds = unsortedThresholds;
        TriggersAlerts = triggersAlerts;
        CurrentThresholdState = currentThresholdState;
        StateAlertDict = stateAlertDict;
        ShowOverlays = showOverlays;
        AllowRevives = allowRevives;
    }
}
