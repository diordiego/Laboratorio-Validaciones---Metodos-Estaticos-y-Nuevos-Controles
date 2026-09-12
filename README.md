# Laboratorio #3 — Validaciones, Métodos Estáticos y Nuevos Controles

**Universidad Tecnológica de Panamá** — Facultad de Ingeniería de Sistemas Computacionales
Herramientas de Programación Aplicada III (.Net) · Grupo 1IL133 · II Semestre 2026

- **Estudiante:** Diego Sanjur
- **Facilitadora:** Irina Fong

---

## Descripción

Este repositorio contiene las tres actividades del Laboratorio #3. A diferencia del laboratorio anterior, las actividades son independientes entre sí y cada una explora un tema distinto: validación de formularios con retroalimentación visual, uso de enumeraciones y números aleatorios en consola, y el modelo de interfaz de múltiples documentos.

El hilo común es la separación de responsabilidades: la lógica de validación vive en una clase estática reutilizable, la lógica del juego vive en su propia clase, y el formulario contenedor delega en las ventanas hijas.

## Estructura del repositorio

```
ListaPersonas/     Registro de colaboradores con validaciones y DataGridView
JuegoDeCraps/      Simulación del juego de dados en consola
LaboratorioMDI/    Ventana contenedora con formularios hijos
```

---

## Actividad 1: Lista de personas

Aplicación de Windows Forms que registra colaboradores y los muestra en un `DataGridView`. Los objetos `Persona` se almacenan en un `ArrayList`, la colección no genérica del espacio de nombres `System.Collections`.

Cada campo se valida antes de crear el objeto. Si un dato es inválido, un `ErrorProvider` coloca el ícono de error junto al control correspondiente, el foco regresa a ese campo y el método se interrumpe con `return` sin agregar el registro. El correo se valida en la clase estática `Utilidades` mediante una expresión regular, y el salario y el ID con `TryParse` para evitar excepciones de formato.

Además de agregar registros, el formulario permite seleccionar una fila del grid para cargarla en los controles y modificarla. Como el `ArrayList` guarda referencias a los objetos y no copias, basta con modificar las propiedades del objeto seleccionado para que el cambio quede aplicado en la colección; luego se reasigna el `DataSource` para refrescar la vista.

**Conceptos:** `ArrayList`, clases estáticas, expresiones regulares, `ErrorProvider`, `DataGridView` con enlace a datos, `TryParse`, tipos por referencia.

**Diagrama UML**

```
┌────────────────────────────────────────────┐
│                 Persona                    │
├────────────────────────────────────────────┤
│ + <<property>> Id: int                     │
│ + <<property>> Nombres: string             │
│ + <<property>> Apellidos: string           │
│ + <<property>> Correo: string              │
│ + <<property>> FechaNacimiento: DateTime   │
│ + <<property>> Salario: decimal            │
└────────────────────────────────────────────┘

┌────────────────────────────────────────────┐
│         <<static>> Utilidades              │
├────────────────────────────────────────────┤
├────────────────────────────────────────────┤
│ + EsCorreoValido(email: string): bool      │
│ + EstaEnBlanco(texto: string): bool        │
└────────────────────────────────────────────┘
```

## Actividad 2: Juego de Craps

Aplicación de consola que simula el juego de dados Craps. En el primer lanzamiento el jugador gana con 7 u 11 y pierde con 2, 3 o 12; cualquier otro resultado se convierte en su *punto*, y a partir de ahí debe repetirlo antes de sacar un 7 para ganar.

Los valores clave de los dados y los estados posibles de la partida se representan con enumeraciones (`enum`) en lugar de números sueltos, lo que hace el `switch` legible sin necesidad de comentarios. El primer lanzamiento se resuelve con esa estructura `switch` y los lanzamientos siguientes con un ciclo `while` que se repite mientras el estado sea `CONTINUA`.

Los dados se simulan con la clase `Random`, generando dos valores entre 1 y 6 en cada tiro.

**Conceptos:** enumeraciones, conversión explícita (*casting*), `switch` con casos agrupados, ciclo `while`, clase `Random`, interpolación de cadenas.

**Diagrama UML**

```
┌────────────────────────────────────────────┐
│                  Craps                     │
├────────────────────────────────────────────┤
│ - numerosAleatorios: Random                │
│ - <<enum>> NombreDados                     │
│ - <<enum>> Estado                          │
├────────────────────────────────────────────┤
│ + Jugar()                                  │
│ + LanzarDados(): int                       │
└────────────────────────────────────────────┘
```

## Actividad 3: Interfaz de Múltiples Documentos (MDI)

Formulario principal configurado como contenedor MDI, desde cuyo `ToolStrip` se abren ventanas hijas dentro del área del formulario padre.

Antes de crear una ventana nueva, el programa consulta `Application.OpenForms` para verificar si ya existe una instancia abierta de ese tipo. Si la encuentra, la trae al frente con `BringToFront()` y le da el foco en lugar de duplicarla; si no existe, crea la instancia, le asigna el formulario actual como `MdiParent` y la muestra.

**Conceptos:** propiedad `IsMdiContainer`, `MdiParent`, colección `Application.OpenForms`, LINQ con `OfType<T>()` y `FirstOrDefault()`, control `ToolStrip`.

**Diagrama UML**

```
┌────────────────────────────────────────────┐
│         Form1 <<MdiContainer>>             │
├────────────────────────────────────────────┤
├────────────────────────────────────────────┤
│ + tsbActivarBoton_Click()                  │
└────────────────────────────────────────────┘
                    │ contiene
                    ▼
┌────────────────────────────────────────────┐
│            frmVentanaTexto                 │
├────────────────────────────────────────────┤
├────────────────────────────────────────────┤
│ + frmVentanaTexto()                        │
└────────────────────────────────────────────┘
```

---

## Requisitos

- .NET 10.0
- Visual Studio 2026 con la carga de trabajo de desarrollo de escritorio de .NET
- Windows (las actividades 1 y 3 utilizan Windows Forms)

## Ejecución

Desde Visual Studio, abrir la solución de la actividad y presionar `F5`.

Desde la línea de comandos, situarse en la carpeta del proyecto y ejecutar:

```bash
dotnet run
```
