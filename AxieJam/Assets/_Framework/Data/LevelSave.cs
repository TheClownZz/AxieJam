[System.Serializable]
public class LevelSave
{
    #region CONST
    #endregion

    #region EDITOR PARAMS
    #endregion

    #region PARAMS
    public int currentLevel;
    public int highestLevel;
    #endregion

    #region PROPERTIES
    public LevelSave(int currentLevelId, int highestLevelId)
    {
        this.currentLevel = currentLevelId;
        this.highestLevel = highestLevelId;
    }
    #endregion

    #region EVENTS
    #endregion

    #region METHODS
    #endregion

    #region DEBUG
    #endregion
}

