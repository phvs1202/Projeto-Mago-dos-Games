const urlParametro = new URLSearchParams(window.location.search);
const infoId = urlParametro.get("id");

class ListaJogos {
    constructor() {
        this.itens = [];
        this.listaTabela = document.getElementById("lista_jogos");
    }

    adicionarJogos(jogo) {
        this.itens.push(jogo);
    }

    atualizarLista() {
        this.listaTabela.innerHTML = "";
        this.itens.forEach((item, index) => {
            this.listaTabela.innerHTML += `
                <tr>
                    <td>${item.nome}</td>
                    <td>${item.preco}</td>
                    <td>
                        <select id="status-${item.idJogo}">
                            <option value="" ${item.status === "" ? "selected" : ""}>Selecione um Status</option>
                            <option value="Já tenho" ${item.status === "Já tenho" ? "selected" : ""}>Já tenho</option>
                            <option value="Quero comprar" ${item.status === "Quero comprar" ? "selected" : ""}>Quero comprar</option>
                            <option value="Já zerei" ${item.status === "Já zerei" ? "selected" : ""}>Já zerei</option>
                        </select>
                        <button onclick="adicionarStatus(${infoId}, ${item.idJogo}, document.getElementById('status-${item.idJogo}').value)" class="add_status">Adicionar status</button>
                    </td>
                </tr>
            `;
        });
    }
}

class Jogos {
    constructor(id, nome, preco, status = "") {
        this.idJogo = id;
        this.nome = nome;
        this.preco = preco;
        this.status = status;
    }
}

const urlRelacao = `https://localhost:7225/api/User_game`;
const urlJogo = `https://localhost:7225/api/Jogos`;

let gerenciadorJogos = new ListaJogos();

// **1. Carregar os relacionamentos de status**
async function carregarRelacoes() {
    const urlUser = `https://localhost:7225/api/User_game/user/${infoId}`;
    try {
        const response = await fetch(urlUser);
        if (!response.ok) {
            throw new Error("Erro ao carregar os status");
        }
        const statusData = await response.json();

        // Criar um mapa associando os status ao idJogo
        let statusMap = {};
        statusData.forEach(item => {
            statusMap[item.jogoid] = item.status;
        });

        return statusMap;
    } catch (error) {
        console.error("Erro ao carregar status:", error);
        return {};
    }
}

// **2. Carregar os jogos e associar os status**
async function carregarJogos() {
    try {
        const response = await fetch(urlJogo);
        if (!response.ok) {
            throw new Error("Erro ao carregar jogos");
        }
        const jogosData = await response.json();
        
        // Carregar os status e associá-los aos jogos
        const statusMap = await carregarRelacoes();

        jogosData.forEach(item => {
            let jogo = new Jogos(item.id, item.nome, item.preco, statusMap[item.id] || "");
            gerenciadorJogos.adicionarJogos(jogo);
        });

        // Atualizar a lista com os status corretos
        gerenciadorJogos.atualizarLista();
    } catch (error) {
        console.error("Erro ao carregar jogos:", error);
    }
}

// **3. Função para adicionar um status**
async function adicionarStatus(idUser, idJogo, status) {
    let dados = {
        userid: idUser,
        jogoid: idJogo,
        status: status
    };

    try {
        const response = await fetch(urlRelacao, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(dados),
        });

        if (!response.ok) {
            throw new Error("Erro ao atualizar status");
        }

        console.log("Status atualizado com sucesso!");
        alert("Status atualizado!");
    } catch (error) {
        console.error("Erro ao adicionar status:", error);
        alert("Erro ao adicionar status.");
    }
}

// **Iniciar carregamento dos dados**
carregarJogos();


carregarJogos();

const nome = document.getElementById("nome_jogo");
const preco = document.getElementById("preco_jogo");
const form = document.getElementById("adiciona_jogo");

form.addEventListener("submit", function(event){
    event.preventDefault();

    let dados = {
        nome: nome.value,
        preco: preco.value
    }

    console.log(dados.nome, dados.preco)

    fetch(urlJogo, {
    method: 'POST',
    headers: {
        'Content-Type': 'application/json',
    },
    body: JSON.stringify(dados),
})
    .then(response => {
        if (!response.ok) {
            throw new Error('Erro na rede');
        }
        alert("Jogo cadastrado com sucesso")
        return response.json();
    })
    .then(data => {
        console.log("sucesso");
    })
    .catch(error => console.error(`Erro: ${error}`));
    window.location.reload();
})

