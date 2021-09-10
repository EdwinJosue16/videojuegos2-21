## Tarea #3
## Creación de videojuegos
## Edwin Josué Brenes Cambronero, B51187


### Solución

- La lógica para se agregó en el archivo Ball.cs
- Los métodos para lograr el objetivo son: 

```
    void OnCollisionEnter2D(Collision2D other)
    void Bounce(ContactPoint2D contact)
```
- El primer método es el que se invoca cuando se produce la colisión contra alguna pared o la paleta. Para conseguir dicha colisión y el efecto deseado, se hicieron tales elementos RigidBody2D y se les agregó box collider, el método en sí lo que hace es tomar el punto donde colisiona la pelota y dicho punto se le envía por parámetro al método Bounce.
- El método bounce, lo que realiza es reflejar el vector de velocidad que lleva la bola. Para esto se utiliza el método Reflect de Vector2, la reflexión requiere el vector con respecto al cual se refleja y dicho vector es el normal a la superficie enel punto de colisión. El vector a reflejar es la velocidad, pero se refleja dicho vector normalizado pues para efectos de la reflexión lo que importa es la dirección que dicha velocidad va tomar.
- Finalmente, en bounce, se multiplica el vector reflejado por la norma de la velocidad inicial. Lo anterior para mantener la velocidad de rebote constante y mayor que 1, de hecho al valor de la velocidad inicial. Este vector final calculado, se asigna como nueva velocidad a la bola.
- En este caso, la lógica para paredes y paleta es la misma y produce el efecto deseado.
