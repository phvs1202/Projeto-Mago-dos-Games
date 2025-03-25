class Jogo {
    constructor(id, nome, preco, status) {
        this.id = id;
        this.nome = nome;
        this.preco = preco;
        this.status = status;
    }
}

class User {
    constructor(id, email, senha) {
        this.id = id;
        this.email = email;
        this.senha = senha;
    }
}


const email = document.getElementById("email");
const senha = document.getElementById("senha");
const botaoLogin = document.getElementById("entrar");

async function FazerLogin() {
    const urlUser = `https://localhost:7225/api/User/${email.value}/${senha.value}`;

    try {
        const response = await fetch(urlUser);

        // Exibir a resposta HTTP para depuração
        console.log("Status da resposta:", response.status);
        const responseText = await response.json();
        console.log("Resposta da API:", responseText);

        if (response.ok)
        {
            window.location.href = `pages/home.html?id=${responseText.id}`
        }
        else
        {
            alert("Usuário não encontrado");
        }
    } 
    catch (error) {
        console.error("Erro ao fazer login:", error);
        alert("Erro ao fazer login. Verifique os dados e tente novamente.");
    }
}

async function CriarUser() {
    let dados = {
        email: email.value,
        senha: senha.value
    }

    const urlUser = `https://localhost:7225/api/User`;
    console.log(dados.email, " ", dados.senha)

    fetch(urlUser, {
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
            return response.json();
        })
        .then(data => {
            console.log("Sucesso");
        })
        .catch(error => alert(`Esse email já existe, escolha outro.`));

    email.value = "";
    senha.value = "";
}
