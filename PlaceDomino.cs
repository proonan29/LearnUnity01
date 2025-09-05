using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class PlaceDomino : MonoBehaviour
{
    public GameObject dominoPrefab;     // The prefab for the domino to be placed
    public GameObject starterPrefab;    // The prefab for the starter domino
    public float donimoDistance = 1.0f; // Distance between placed dominoes
    public float starterDistance = 0.5f; // Distance between starter & first dominoes
    public SplineContainer spline;
    public float dispY = 0.0f; // Vertical displacement for dominoes

    public void GenerateDominoes()
    {
        // place domino object along the spline
        float gap = starterDistance;

        for (float p = 0; p < spline.Spline.GetLength(); p += gap)
        {
            float3 position;
            float3 upVector;
            float3 tangent;

            // Get the position and rotation of the spline at parameter t
            float t = p / spline.Spline.GetLength();
            spline.Evaluate(t, out position, out tangent, out upVector);
            position.y += dispY; // Apply vertical displacement
            //Vector3 rotation = spline.Spline.EvaluateCurvatureCenter(p);

            // convert tangent to a y-rotation
            float angle = Mathf.Atan2(tangent.x, tangent.z) * Mathf.Rad2Deg;

            if (p == 0 && starterPrefab != null)
            {
                position.y += 0.2f; // Slightly raise the starter domino

                // Instantiate the starter prefab at the calculated position and rotation
                GameObject starter = Instantiate(starterPrefab, position, Quaternion.Euler(0, angle, 0));
                // Optionally, set the parent of the starter to this GameObject
                starter.transform.parent = this.transform;
            }
            else
            {
                gap = donimoDistance;

                // Instantiate the domino prefab at the calculated position and rotation
                GameObject domino = Instantiate(dominoPrefab, position, Quaternion.Euler(0, angle, 0));
                // Optionally, set the parent of the domino to this GameObject
                domino.transform.parent = this.transform;
            }
        }
    }
}
