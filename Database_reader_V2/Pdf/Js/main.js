﻿const url = 'Turing.pdf'; //prova

//const url localStorage.getItem("Pdf_link")      --->una volta messo a posto uso questo per ottenere il link

let pdfDoc = null,
    pageNum = 1,
    pageIsRendering = false,
    end=false,
    pageNumIsPending = null;

//sessionStorage.setItem('end_pages', end); ---non aggiorna status

const scale = 1.5,
    canvas = document.querySelector('#pdf-render'),
    ctx = canvas.getContext('2d');

// Render the page
const renderPage = num => {
    pageIsRendering = true;

    // Get page
    pdfDoc.getPage(num).then(page => {
        // Set scale
        const viewport = page.getViewport({ scale });
        canvas.height = viewport.height;
        canvas.width = viewport.width;

        const renderCtx = {
            canvasContext: ctx,
            viewport
        };

        page.render(renderCtx).promise.then(() => {
            pageIsRendering = false;

            if (pageNumIsPending !== null) {
                renderPage(pageNumIsPending);
                pageNumIsPending = null;
            }
        });

        // Output current page
        document.querySelector('#page-num').textContent = num;
    });
};

// Check for pages rendering
const queueRenderPage = num => {
    if (pageIsRendering) {
        pageNumIsPending = num;
    } else {
        renderPage(num);
    }
};

// Show Prev Page
const showPrevPage = () => {
    if (pageNum <= 1) {
        return;
    }
    pageNum--;
    queueRenderPage(pageNum);
};

// Show Next Page
const showNextPage = () => {
    if (pageNum >= pdfDoc.numPages) {
        return;
    }
    pageNum++;
    queueRenderPage(pageNum);
    if (pageNum == pdfDoc.numPages) {
        end = true;
        parent.document.getElementById("validate").disabled = false;
        parent.document.getElementById("review").disabled = false; //a quanto pare si può risolvere la cosa senza ajax ma utilizzando la funziona parent di hmtl
        //document.getElementById("validate").disabled = false;
       // document.getElementById("review").disabled = false;
    }
};

// Get Document
pdfjsLib
    .getDocument(url)
    .promise.then(pdfDoc_ => {
        pdfDoc = pdfDoc_;

        document.querySelector('#page-count').textContent = pdfDoc.numPages;

        renderPage(pageNum);
    })
    .catch(err => {
        // Display error
        const div = document.createElement('div');
        div.className = 'error';
        div.appendChild(document.createTextNode(err.message));
        document.querySelector('body').insertBefore(div, canvas);
        // Remove top bar
        document.querySelector('.top-bar').style.display = 'none';
    });

// Button Events
document.querySelector('#prev-page').addEventListener('click', showPrevPage);
document.querySelector('#next-page').addEventListener('click', showNextPage);



//parte relativa al controllo pagina

function verifica() {
    sessionStorage.setItem('end_pages', end);
    document.querySelector('#end').textContent = end;
}
document.querySelector('#esegui').addEventListener('click', verifica);