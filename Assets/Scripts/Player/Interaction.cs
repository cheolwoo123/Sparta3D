using TMPro;                         // TextMeshPro UI�� ����ϱ� ���� ���ӽ����̽�
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f; // ����ĳ��Ʈ�� �󸶳� ���� �������� ���� (0.05�� = 20������ ����)
    private float lastCheckTime;    // ���������� ����ĳ��Ʈ�� ������ �ð�

    public float maxCheckDistance = 5f; // ī�޶� �� �������� �󸶳� �ָ����� �������� ���� (����: ����Ƽ �Ÿ� ����)

    public LayerMask layerMask;     // � ���̾ �������� ���� (��: "Interactable" ���̾ ����)

    public TextMeshProUGUI promptText; // ������Ʈ ������ ǥ���� TextMeshPro UI ��� (�����Ϳ��� ����)

    private Camera mainCamera;         // ���� ī�޶� ����

    private GameObject curTarget;      // ���� ������ ������Ʈ

    void Start()
    {
        mainCamera = Camera.main;      // ������ �� ���� ī�޶� ã�� ����
    }

    void Update()
    {
        // ���� �ð����ٸ� ����ĳ��Ʈ ����
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            // ȭ�� �߾ӿ��� ī�޶� �������� Ray�� ���
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

            // ����ĳ��Ʈ�� �����ؼ� ������Ʈ�� �����Ǹ� hit�� ���� ����
            if (Physics.Raycast(ray, out RaycastHit hit, maxCheckDistance, layerMask))
            {
                // ������ ������ ������Ʈ�� �ٸ� ���� ���� ǥ��
                if (hit.collider.gameObject != curTarget)
                {
                    curTarget = hit.collider.gameObject;

                    // �̸� + ���� ǥ��
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
                // ������ ������Ʈ�� ���� ��� �ʱ�ȭ
                curTarget = null;
                promptText.gameObject.SetActive(false); // UI ��Ȱ��ȭ
            }
        }
    }

    // UI �ؽ�Ʈ�� ǥ���ϴ� �Լ�
    void ShowPrompt(string message)
    {
        promptText.gameObject.SetActive(true); // UI ���̰� ����
        promptText.text = message;             // �ؽ�Ʈ ������Ʈ
    }
}
