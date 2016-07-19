using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(LineRenderer))]
public class Pendulum : MonoBehaviour {
    public Transform joint;
    private Rigidbody2D _body;
    private LineRenderer _lineRenderer;

    private void Start() {
        _body = GetComponent<Rigidbody2D>();

        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.SetWidth(0.1f, 0.1f);
    }

    private void FixedUpdate() {
        var pos3d = joint.transform.position - transform.position;
        var pos = new Vector2(pos3d.x, pos3d.y);

        // Centripetal force using the  m*v^2/r  formula (our mass is 1)
        var centripetalMagnitude = Mathf.Pow(_body.velocity.magnitude, 2)/pos.magnitude;

        pos.Normalize();

        // The part of the force that counters gravity depends on the angle of the pendulum
        var acos = Vector2.Dot(-pos, Physics2D.gravity.normalized);

        // Combining the forces should give us the resulting force that causes the pendulum to swing
        var force = pos*acos*Physics2D.gravity.magnitude + pos*centripetalMagnitude;
        _body.AddForce(force);

        // This is simply for debug drawing
        _lineRenderer.SetPositions(new[] {
            transform.position,
            joint.transform.position
        });

    }
}