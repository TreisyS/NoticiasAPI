const uri = 'https://localhost:7014/api/Noticias';

let noticias = [];

function getItems() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayNoticias(data))
        .catch(error => console.error('No se pueden obtener las noticias.', error));
}

function addNoticia() {
    const addTituloTextbox = document.getElementById('add-titulo');
    const addPaisTextbox = document.getElementById('add-pais');
    // Agrega más variables para otros campos según sea necesario

    const noticia = {
        Titulo: addTituloTextbox.value.trim(),
        Pais: addPaisTextbox.value.trim(),
        // Agrega más campos según sea necesario
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(noticia)
    })
        .then(response => response.json())
        .then(() => {
            getItems();
            // Limpiar los campos del formulario después de agregar
            addTituloTextbox.value = '';
            addPaisTextbox.value = '';
            // Limpiar más campos según sea necesario
        })
        .catch(error => console.error('No se puede agregar la noticia.', error));
}


function deleteNoticia(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getItems())
        .catch(error => console.error('No se puede eliminar la noticia.', error));
}

function displayEditForm(id) {
    const noticia = noticias.find(item => item.id === id);

    document.getElementById('edit-titulo').value = noticia.titulo;
    document.getElementById('edit-pais').value = noticia.pais;
    // Asigna valores a más campos según sea necesario
    document.getElementById('edit-id').value = noticia.id;
    document.getElementById('editForm').style.display = 'block';
}

function updateNoticia() {
    const noticiaId = document.getElementById('edit-id').value;
    const noticia = {
        id: parseInt(noticiaId, 10),
        titulo: document.getElementById('edit-titulo').value.trim(),
        pais: document.getElementById('edit-pais').value.trim(),
        // Asigna valores a más campos según sea necesario
    };

    fetch(`${uri}/${noticiaId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(noticia)
    })
        .then(() => getItems())
        .catch(error => console.error('No se puede actualizar la noticia.', error));

    closeInput();
    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'noticia' : 'noticias';
    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayNoticias(data) {
    const tbody = document.getElementById('noticias');
    tbody.innerHTML = '';
    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(noticia => {
        let tr = tbody.insertRow();

        let tdId = tr.insertCell(0);
        tdId.appendChild(document.createTextNode(noticia.id));

        let tdTitulo = tr.insertCell(1);
        tdTitulo.appendChild(document.createTextNode(noticia.titulo));

        let tdPais = tr.insertCell(2);
        tdPais.appendChild(document.createTextNode(noticia.pais));

        let tdCategoria = tr.insertCell(3);
        tdCategoria.appendChild(document.createTextNode(noticia.categoria));

        let tdFecha = tr.insertCell(4);
        tdFecha.appendChild(document.createTextNode(noticia.fecha));

        let tdFuente = tr.insertCell(5);
        tdFuente.appendChild(document.createTextNode(noticia.fuente));

        let tdContenido = tr.insertCell(6);
        tdContenido.appendChild(document.createTextNode(noticia.contenido));

        let tdEnlace = tr.insertCell(7);
        tdEnlace.appendChild(document.createTextNode(noticia.enlace));

        let tdAutor = tr.insertCell(8);
        tdAutor.appendChild(document.createTextNode(noticia.autor));

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Editar';
        editButton.setAttribute('onclick', `displayEditForm(${noticia.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Eliminar';
        deleteButton.setAttribute('onclick', `deleteNoticia(${noticia.id})`);

        let tdEdit = tr.insertCell(3);
        tdEdit.appendChild(editButton);

        let tdDelete = tr.insertCell(4);
        tdDelete.appendChild(deleteButton);
    });

    noticias = data;
}

