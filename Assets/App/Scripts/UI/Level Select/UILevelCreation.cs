using UnityEngine;

public class UILevelCreation : MonoBehaviour {
    [SerializeField]
    private UILevelData template; // We will clone this . . .
    [SerializeField]
    private LevelDatabase levelDatabase;
    [SerializeField]
    private RectTransform content; // This is the reference to the 'Content' where the level buttons exist . . .

    void Start() {
        //var levels 
        var levels = this.levelDatabase.Levels;
        var iterator = levels.GetEnumerator();

        while (iterator.MoveNext()) {
            var levelData = iterator.Current;

            var clone = Instantiate(this.template.gameObject);
            clone.SetActive(true);

            clone.transform.SetParent(this.content);
            clone.transform.localScale = Vector3.one; //reset the scale back to one since it'll be canvas scale not parent scale

            var uiLevelData = clone.GetComponent<UILevelData>();

            uiLevelData.SetLevelData(levelData);
        }
    }
}
