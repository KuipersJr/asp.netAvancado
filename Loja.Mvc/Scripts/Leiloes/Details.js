// kyle Simpson
// object literal

var Details = {
    produtoId: 0,
    nomeParticipante: "",
    leilaoHub: {},
    connectionId:"",

    inicializar: function (produtoId) {
        this.produtoId = produtoId;
        this.conectarLeilaoHub();
        this.vincularEventos();
    },
    
    conectarLeilaoHub: function () {
        var self = this;
        var connection = $.hubConnection();
        this.leilaoHub = connection.createHubProxy("LeilaoHub");

        //hub.on("atualizarOfertas", function () { document.location.reload(); });
        connection.start().done(function () { self.connectionId = connection.id });
    },

    vincularEventos: function () {
        var self = this;
        $("#entrarButton").on("click", function () { self.entrarLeilao(); })
        this.leilaoHub.on("adicionarMensagem",
                          function (nomeRemetente, connectionId, mensagem)
                          { self.adicionarMensagem(nomeRemetente, connectionId, mensagem) })

        $("#enviarLanceButton").on("click", function () { self.realizarLance(); })

        this.leilaoHub.on("receberLike", function (nomereRemetente) { self.receberLike(nomereRemetente) });

        $(document).on("click", "a[data-connection-Id]",
            function () {
                self.enviarLike($(this).data("connection-id"));
            });
    },

    entrarLeilao: function () {
        this.nomeParticipante = $("#nomeParticipante").val();
        this.leilaoHub.invoke("Participar", this.nomeParticipante, this.produtoId);

        $("#participanteDiv").hide();
        $("#lanceDiv").show();
        $("#valorLance").focus();
    },

    adicionarMensagem: function (nomeRemetente,connectionId,mensagem) {
        $("#lancesRealizadosTable").append(this.montarMensagem(nomeRemetente, connectionId, mensagem));
    },

    montarMensagem: function (nomeRemetente, connectionId, mensagem) {
        var tr = "<tr>";
        tr += "<td>" + nomeRemetente + "</td>";
        tr += "<td>" + mensagem + "</td>";

        var like = "<a data-connection-id='" + connectionId + "' href='#'>" +
                    "<span class='glyphicon glyphicon-thumbs-up' style='font-size:18px'></span></a>";
        var enviadaPorMim = this.connectionId === connectionId;

        tr += "<td>" + (enviadaPorMim ? "" : like) + "</td>";

        tr += "</tr>";

        return tr;
    },

    enviarLike: function (connectionIdDestinatario) {
        this.leilaoHub.invoke("EnviarLike", this.nomeParticipante, connectionIdDestinatario);
    },

    realizarLance: function () {
        this.leilaoHub.invoke("realizarLance", this.nomeParticipante,
            this.connectionId, $("#valorLance").val(), this.produtoId);
    },

    receberLike: function (nomeRemetente) {
        $("#sinoNotificacoes")
            .popover("destroy")
            .popover({
                content: "<span class='glyphicon glyphicon-thumbs-up' style='font-size:24px'></span>",
                html: true,
                placement: "left",
                title: nomeRemetente + " diz:"
            })
            .popover("show");
    }

};
