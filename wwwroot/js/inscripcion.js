console.log("inscripcion.js cargado correctamente");


// 1. Referencias al DOM

const selectCurso = document.getElementById("CursoId");
const checkClases = document.getElementById("ClasesExtra");
const checkMaterial = document.getElementById("Material");
const resumenCosto = document.getElementById("resumenCosto");
const formInscripcion = document.getElementById("formInscripcion");


const PRECIO_CLASES_EXTRA = Number(resumenCosto.dataset.clases);
const PRECIO_MATERIAL = Number(resumenCosto.dataset.material);

// 2. Funciones

function formatearDinero(valor) {
    return "$" + valor.toLocaleString("es-CO");
}

function actualizarResumen() {
    const opcion = selectCurso.options[selectCurso.selectedIndex];

    if (!opcion || opcion.value === "") {
        resumenCosto.innerHTML = `<p class="mb-0">Selecciona un curso para calcular el valor.</p>`;
        return;
    }

    const precio = Number(opcion.dataset.precio);
    let total = precio;

    if (checkClases.checked) {
        total += PRECIO_CLASES_EXTRA;
    }
    if (checkMaterial.checked) {
        total += PRECIO_MATERIAL;
    }

    resumenCosto.innerHTML = `
        <p class="mb-1"><strong>${opcion.text}</strong></p>
        <p class="mb-1">Duración: ${opcion.dataset.duracion}</p>
        <p class="mb-1">Valor del curso: ${formatearDinero(precio)}</p>
        <p class="mb-1">Clases adicionales: ${checkClases.checked ? formatearDinero(PRECIO_CLASES_EXTRA) : "No"}</p>
        <p class="mb-3">Material impreso: ${checkMaterial.checked ? formatearDinero(PRECIO_MATERIAL) : "No"}</p>
        <p class="total mb-0">Total: ${formatearDinero(total)}</p>
    `;
}

// 3. Eventos

selectCurso.addEventListener("change", actualizarResumen);
checkClases.addEventListener("change", actualizarResumen);
checkMaterial.addEventListener("change", actualizarResumen);


formInscripcion.addEventListener("reset", () => {
    setTimeout(actualizarResumen, 0);
});

// 4. Inicio

actualizarResumen();