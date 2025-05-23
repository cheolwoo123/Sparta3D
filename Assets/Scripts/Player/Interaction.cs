using TMPro;                         // TextMeshPro UI를 사용하기 위한 네임스페이스
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f; // 레이캐스트를 얼마나 자주 갱신할지 설정 (0.05초 = 20프레임 간격)
    private float lastCheckTime;    // 마지막으로 레이캐스트를 수행한 시간

    public float maxCheckDistance = 5f; // 카메라 앞 방향으로 얼마나 멀리까지 감지할지 설정 (단위: 유니티 거리 단위)

    public LayerMask layerMask;     // 어떤 레이어만 감지할지 설정 (예: "Interactable" 레이어만 감지)

    public TextMeshProUGUI promptText; // 오브젝트 정보를 표시할 TextMeshPro UI 요소 (에디터에서 연결)

    private Camera mainCamera;         // 메인 카메라 참조

    private GameObject curTarget;      // 현재 감지된 오브젝트

    void Start()
    {
        mainCamera = Camera.main;      // 시작할 때 메인 카메라를 찾아 참조
    }

    void Update()
    {
        // 일정 시간마다만 레이캐스트 수행
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            // 화면 중앙에서 카메라 방향으로 Ray를 쏜다
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

            // 레이캐스트를 실행해서 오브젝트가 감지되면 hit에 정보 저장
            if (Physics.Raycast(ray, out RaycastHit hit, maxCheckDistance, layerMask))
            {
                // 이전에 감지한 오브젝트와 다를 때만 새로 표시
                if (hit.collider.gameObject != curTarget)
                {
                    curTarget = hit.collider.gameObject;

                    // 이름 + 설명 표시
                    var info = curTarget.GetComponent<InspectableInfo>();
                    if (info != null )
                    {
                        ShowPrompt($"<b>{info.objectName}</b>\n\n{info.description}");
                    }
                    else
                    {
                        ShowPrompt(curTarget.name);
                    }

                }
            }
            else
            {
                // 감지된 오브젝트가 없을 경우 초기화
                curTarget = null;
                promptText.gameObject.SetActive(false); // UI 비활성화
            }
        }
    }

    // UI 텍스트를 표시하는 함수
    void ShowPrompt(string message)
    {
        promptText.gameObject.SetActive(true); // UI 보이게 설정
        promptText.text = message;             // 텍스트 업데이트
    }
}
