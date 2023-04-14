//Class for positive and negative index of blendshapes
public class Blendshape
{
    //The index of the max suffixes of the blendshapes. Their position inside the blendshape list in the inspector
    public int positiveIndex { get; set; }
    //The index of the min suffixes of the blendshapes. Their position inside the blendshape list in the inspector
    public int negativeIndex { get; set; }

    public Blendshape(int positiveIndex, int negativeIndex)
    {
            this.positiveIndex = positiveIndex;
            this.negativeIndex = negativeIndex;  
    }

} 