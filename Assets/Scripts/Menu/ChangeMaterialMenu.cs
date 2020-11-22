using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialMenu : MonoBehaviour
{

    [Header("Desafio")]
    [SerializeField] Material desafioMaterialSelected1;
    [SerializeField] Material desafioMaterialNotSelected1;
    [SerializeField] Material desafioMaterialSelected2;
    [SerializeField] Material desafioMaterialNotSelected2;
    [SerializeField] MeshRenderer desafioRenderer1;
    [SerializeField] MeshRenderer desafioRenderer2;

    [Header("Loja")]
    [SerializeField] Material lojaMaterialSelected1;
    [SerializeField] Material lojaMaterialNotSelected1;
    [SerializeField] Material lojaMaterialSelected2;
    [SerializeField] Material lojaMaterialNotSelected2;
    [SerializeField] MeshRenderer lojaRenderer1;
    [SerializeField] MeshRenderer lojaRenderer2;

    [Header("Luta")]
    [SerializeField] Material lutaMaterialSelected1;
    [SerializeField] Material lutaMaterialNotSelected1;
    [SerializeField] Material lutaMaterialSelected2;
    [SerializeField] Material lutaMaterialNotSelected2;
    [SerializeField] MeshRenderer lutaRenderer1;
    [SerializeField] MeshRenderer lutaRenderer2;

    [Header("Trofeu")]
    [SerializeField] Material trofeuMaterialSelected1;
    [SerializeField] Material trofeuMaterialNotSelected1;
    [SerializeField] Material trofeuMaterialSelected2;
    [SerializeField] Material trofeuMaterialNotSelected2;
    [SerializeField] MeshRenderer trofeuRenderer1;
    [SerializeField] MeshRenderer trofeuRenderer2;

    [Header("Configuration")]
    [SerializeField] Material configMaterialSelected1;
    [SerializeField] Material configMaterialNotSelected1;
    [SerializeField] Material configMaterialSelected2;
    [SerializeField] Material configMaterialNotSelected2;
    [SerializeField] MeshRenderer configRenderer1;
    [SerializeField] MeshRenderer configRenderer2;

    [Header("Save")]
    [SerializeField] Material saveMaterialSelected1;
    [SerializeField] Material saveMaterialNotSelected1;
    [SerializeField] Material saveMaterialSelected2;
    [SerializeField] Material saveMaterialNotSelected2;
    [SerializeField] MeshRenderer saveRenderer1;
    [SerializeField] MeshRenderer saveRenderer2;

    [Header("Customization")]
    [SerializeField] Material customMaterialSelected1;
    [SerializeField] Material customMaterialNotSelected1;
    [SerializeField] Material customMaterialSelected2;
    [SerializeField] Material customMaterialNotSelected2;
    [SerializeField] MeshRenderer customRenderer1;
    [SerializeField] MeshRenderer customRenderer2;

    
    public void DeselectPredio()
    {
        desafioRenderer1.material = desafioMaterialNotSelected1;
        desafioRenderer2.material = desafioMaterialNotSelected2;
    }
    public void SelectPredio()
    {
        desafioRenderer1.material = desafioMaterialSelected1;
        desafioRenderer2.material = desafioMaterialSelected2;
    }
    public void DeselectLoja()
    {
        lojaRenderer1.material = lojaMaterialNotSelected1;
        lojaRenderer2.material = lojaMaterialNotSelected2;
    }
    public void SelectLoja()
    {
        lojaRenderer1.material = lojaMaterialSelected1;
        lojaRenderer2.material = lojaMaterialSelected2;
    }
    public void DeselectLuta()
    {
        lutaRenderer1.material = lutaMaterialNotSelected1;
        lutaRenderer2.material = lutaMaterialNotSelected2;
    }
    public void SelectLuta()
    {
        print(gameObject.name);
        lutaRenderer1.material = lutaMaterialSelected1;
        lutaRenderer2.material = lutaMaterialSelected2;
    }
    public void DeselectTrofeu()
    {
        trofeuRenderer1.material = trofeuMaterialNotSelected1;
        trofeuRenderer2.material = trofeuMaterialNotSelected2;
    }
    public void SelectTrofeu()
    {
        trofeuRenderer1.material = trofeuMaterialSelected1;
        trofeuRenderer2.material = trofeuMaterialSelected2;
    }
    public void DeselectConfig()
    {
        configRenderer1.material = configMaterialNotSelected1;
        configRenderer2.material = configMaterialNotSelected2;
    }
    public void SelectedConfig()
    {
        configRenderer1.material = configMaterialSelected1;
        configRenderer2.material = configMaterialSelected2;
    }
    public void DeselectSave()
    {
        saveRenderer1.material = saveMaterialNotSelected1;
        saveRenderer2.material = saveMaterialNotSelected2;
    }
    public void SelectedSave()
    {
        saveRenderer1.material = saveMaterialSelected1;
        saveRenderer2.material = saveMaterialSelected2;
    }
    public void DeselectCustom()
    {
        customRenderer1.material = customMaterialNotSelected1;
        customRenderer2.material = customMaterialNotSelected2;
    }
    public void SelectedCustom()
    {
        customRenderer1.material = customMaterialSelected1;
        customRenderer2.material = customMaterialSelected2;
    }
   
}
