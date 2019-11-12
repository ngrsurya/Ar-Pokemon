using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PokemonActivityManager : MonoBehaviour
{
    [Tooltip("Link the UI text object here")]
    public Text messageDisplay;
    [Tooltip( "Link the particle system prefab here" )]
    public GameObject particles;

    [Header("List of pokemon names")]
    [Tooltip("Set the number of names and key in the pokemon name in each Element field")]
    public string[] names;

    [Header("What happens when we match")]
    public UnityEvent onMatch;
    [Header( "What happens when we mismatch" )]
    public UnityEvent onMismatch;

    [Header("How long should correct/message show")]
    [Tooltip("Time in seconds")]
    [Range( 0, 10f )]
    public float changeDelay = 1f;

    [Header("Text colors")]
    public Color defaultColor;
    public Color correctColor;
    public Color incorrectColor;

    private string selectedName;

    void Start()
    {
        messageDisplay.color = defaultColor;
        ChangeName();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SubmitName( string message )
    {
        if(message == selectedName)
        {
            _onMatch();
        } else
        {
            _onMismatch();
        }
    }

    public void CreateParticles()
    {
        Instantiate( particles );
    }

    private void _onMatch()
    {
        onMatch.Invoke();
        StartCoroutine( "CorrectRoutine" );

    }

    private void _onMismatch()
    {
        onMismatch.Invoke();
        StartCoroutine( "IncorrectRoutine" );
    }

    private void ChangeName()
    {
        if(names.Length < 2)
        {
            Debug.LogWarning( "need more than 1 pokemon" );
            return;
        }

        if(selectedName == null)
        {
            selectedName = names[ Random.Range( 0, names.Length ) ];
        } else
        {
            int newChoice = Random.Range( 0, names.Length );
            if(newChoice == System.Array.IndexOf( names, selectedName ))
            {
                newChoice++;
            }

            if(newChoice >= names.Length)
            {
                newChoice = 0;
            }

            selectedName = names[ newChoice ];
        }
        messageDisplay.text = selectedName;
    }

    private IEnumerator CorrectRoutine()
    {
        messageDisplay.text = "Correct";
        messageDisplay.color = correctColor;
        yield return new WaitForSeconds( changeDelay );
        messageDisplay.color = defaultColor;
        ChangeName();
    }
    private IEnumerator IncorrectRoutine()
    {
        messageDisplay.text = "Wrong";
        messageDisplay.color = incorrectColor;
        yield return new WaitForSeconds( changeDelay );
        messageDisplay.color = defaultColor;
        messageDisplay.text = selectedName;
    }
}
