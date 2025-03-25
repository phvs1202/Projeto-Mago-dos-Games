const forms = document.getElementById('forms');
const criar = document.getElementById('criar');
const entrar = document.getElementById('entrar');

function mostra_forms() {
    forms.style.display = "grid"; // Corrigido de "gird" para "grid"
}

function esconder_forms() {
    forms.style.display = "none";
}

// criar.addEventListener('click', mostra_forms);
// entrar.addEventListener('click', mostra_forms);
