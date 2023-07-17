using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BuildManager : MonoBehaviour
{
    public TurretData MissileTurretData;
    public TurretData StandardTurretData;
    public TurretData LayserTurretData;
    public TurretData SelectTurret;
    private TurretData ClearTurretData;

    private MapCube SelectedMapCube;
    public int Money;
    public Text MoneyText;
    public Animator animator;

    public GameObject UpdateCAnvas;

    public Button TurretUpgradeButton;
    public Button TurretDestructionButton;


    public Animator UpgradeCanvasAni;


    private void Start()
    {
        MoneyText.text = "$" + Money.ToString();
        UpgradeCanvasAni = UpdateCAnvas.GetComponent<Animator>();
    }
    void ChangeMoney(int change = 0)
    {
        Money += change;

        MoneyText.text = "$" + Money;

    }
    private void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)//判断鼠标是否与拥有EventSystem的物体重合
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//鼠标位置发射一条射线
                RaycastHit hit;

                bool isCollider = Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("MapCubeLayer"));
                //通过Raycast得到射线是否碰撞到Layer为mapcubelayer的物体
                print(isCollider);
                if (isCollider)
                {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    //如果碰撞到则得到这个物体的信息
                    if (mapCube.TurretGO == null && SelectTurret!=null)
                    {
                        if (Money > SelectTurret.Cost)
                        {
                            //如果剩余钱够则可以买
                            ChangeMoney(-SelectTurret.Cost);
                            mapCube.TurretBuild(SelectTurret);
                        }
                        else
                        {//TODO提示钱不够
                            animator.SetTrigger("Flicker");
                        }

                    }
                    else if (mapCube.TurretGO != null)
                    {
                        //TODO upgrade
                        if (mapCube == SelectedMapCube && UpdateCAnvas.activeInHierarchy)
                        {//当地板已经放置炮台并且已经打开面板的时候
                            StartCoroutine(HideUpGradeUi());
                        }
                        else
                        {
                            ShowUpgradeUI(mapCube.transform.position, mapCube.isUpgraded);
                        }
                        SelectedMapCube = mapCube;
                    }
                }
            }
        }
    }
    public void OnLayserTurretTouch(bool isON)
    {

        if (isON)
        {
            SelectTurret = LayserTurretData;
        }

    }
    public void OnStandardTurretTouch(bool isON)
    {

        if (isON)
        {
            SelectTurret = StandardTurretData;
        }

    }
    public void OnMissileTurretTouch(bool isON)
    {

        if (isON)
        {
            SelectTurret = MissileTurretData;
        }
    }

    void ShowUpgradeUI(Vector3 ButtonPos, bool isDIsableUpgrade = false)
    {
        StopCoroutine("HideUpGradeUi");
        UpdateCAnvas.SetActive(false);
        //先暂停其他正在使用的hideUI的携程，然后再把升级面板的画布禁用掉

        UpdateCAnvas.SetActive(true);
        UpdateCAnvas.transform.position = ButtonPos;
        TurretUpgradeButton.interactable = !isDIsableUpgrade;
    }

    IEnumerator HideUpGradeUi()
    {
        UpgradeCanvasAni.SetTrigger("Hide");
        yield return new WaitForSeconds(0.8f);
        UpdateCAnvas.SetActive(false);
    }

    public void OnUpgradeButtonDown()
    {
        if (Money >= SelectedMapCube.TurretData.CostUpgrade)
        {
            ChangeMoney(-SelectedMapCube.TurretData.CostUpgrade);
            SelectedMapCube.UpgradeTurret();

        }
        else animator.SetTrigger("Flicker");
        HideUpGradeUi();
        StartCoroutine(HideUpGradeUi());
    }

    public void OnDestroyButtonDown()
    {
        SelectedMapCube.DestoryTurret();
        StartCoroutine(HideUpGradeUi());
    }
}
