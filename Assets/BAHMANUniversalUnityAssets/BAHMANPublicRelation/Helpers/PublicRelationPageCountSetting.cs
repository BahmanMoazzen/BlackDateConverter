using System;
[Serializable]
public class PublicRelationPageCountSetting 
{
    public AllScenes SceneName;
    public PublicRelationType RelationType;
    public int SceneCount;
}

public enum PublicRelationType { Share, Rate, OtherProduct, Donate, Message }

