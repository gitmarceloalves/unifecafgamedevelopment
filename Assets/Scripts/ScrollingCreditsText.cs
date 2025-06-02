using UnityEngine;
using UnityEngine.UI;

public class ScrollingCreditsText : MonoBehaviour
{
    [Header("Configurações")]
    public float scrollSpeed = 50f;
    public float startDelay = 2f;
    public bool startOnEnable = true;
    
    [Header("Texto dos Créditos")]
    [TextArea(10, 20)]
    public string creditsText = 
        "CRÉDITOS\n\n" +
        "Desenvolvido por\nSeu Nome\n\n" +
        "Programação\nSeu Nome\n\n" +
        "Arte\nNome do Artista\n\n" +
        "Música\nNome do Compositor\n\n" +
        "Obrigado por jogar!";
    
    private Text textComponent;
    private RectTransform rectTransform;
    private float startPositionY;
    private float endPositionY;
    private bool isScrolling = false;
    
    void Awake()
    {
        textComponent = GetComponent<Text>();
        rectTransform = GetComponent<RectTransform>();
        
        // Define o texto
        textComponent.text = creditsText;
    }
    
    void Start()
    {
        SetupScrolling();
        
        if (startOnEnable)
        {
            StartScrolling();
        }
    }
    
    void SetupScrolling()
    {
        // Posição inicial (abaixo da tela)
        Canvas canvas = GetComponentInParent<Canvas>();
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        
        startPositionY = -canvasRect.rect.height / 2 - rectTransform.rect.height;
        endPositionY = canvasRect.rect.height / 2 + rectTransform.rect.height;
        
        // Define posição inicial
        Vector2 pos = rectTransform.anchoredPosition;
        pos.y = startPositionY;
        rectTransform.anchoredPosition = pos;
    }
    
    public void StartScrolling()
    {
        if (!isScrolling)
        {
            Invoke(nameof(BeginScroll), startDelay);
        }
    }
    
    void BeginScroll()
    {
        isScrolling = true;
    }
    
    void Update()
    {
        if (isScrolling)
        {
            // Move o texto para cima
            Vector2 pos = rectTransform.anchoredPosition;
            pos.y += scrollSpeed * Time.deltaTime;
            rectTransform.anchoredPosition = pos;
            
            // Verifica se terminou
            if (pos.y >= endPositionY)
            {
                ResetScroll();
            }
        }
    }
    
    public void ResetScroll()
    {
        isScrolling = false;
        Vector2 pos = rectTransform.anchoredPosition;
        pos.y = startPositionY;
        rectTransform.anchoredPosition = pos;
    }
    
    public void StopScrolling()
    {
        isScrolling = false;
    }
    
    // Chame este método quando quiser reiniciar o scroll
    public void RestartScrolling()
    {
        ResetScroll();
        StartScrolling();
    }
}