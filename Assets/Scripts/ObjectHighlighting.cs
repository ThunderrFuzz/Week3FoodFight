using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectHighlighting : MonoBehaviour
{
    public Player player;
    public Material highlightMaterial;
    public LayerMask foodlayer;
    private Material originalMaterialHighlight;
    private Transform highlight;
    private RaycastHit raycastHit;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {
            // Highlight the plate when hovered over
            if (raycastHit.transform.CompareTag("Plate"))
            {
                // Store the original material and set highlight material if not already highlighted
                if (highlight != raycastHit.transform)
                {
                    if (highlight != null)
                    {
                        // Reset previous highlight by setting the matieral back
                        highlight.GetComponent<MeshRenderer>().material = originalMaterialHighlight;
                    }
                    originalMaterialHighlight = raycastHit.transform.GetComponent<MeshRenderer>().material;
                    raycastHit.transform.GetComponent<MeshRenderer>().material = highlightMaterial;
                    highlight = raycastHit.transform;
                }
            }
            else if (highlight != null)
            {
                // Reset highlight if not hovering over a plate
                highlight.GetComponent<MeshRenderer>().material = originalMaterialHighlight;
                highlight = null;
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (raycastHit.transform.CompareTag("Food"))
                {
                    // Set clicked on food true for use elsewhere
                    player.clickedOnFood = true;
                    player.addAmmo();
                    // sets the current held food
                    player.setHeldFood(raycastHit.transform.gameObject);
                    raycastHit.transform.gameObject.SetActive(false);
                }
            }
        }
       
        /* // Selection on click changes material but not needed 
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (highlight)
            {
                if (selection != null)
                {
                    selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                }
                selection = raycastHit.transform;
                if (selection.GetComponent<MeshRenderer>().material != selectionMaterial)
                {
                    originalMaterialSelection = originalMaterialHighlight;
                    selection.GetComponent<MeshRenderer>().material = selectionMaterial;
                }
                highlight = null;
            }
            else
            {
                if (selection)
                {
                    selection.GetComponent<MeshRenderer>().material = originalMaterialSelection;
                    selection = null;
                }
            }
        }*/
    }
}
