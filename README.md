# Construtora Viver SA

A Construtora Viver S/A é uma empresa fictícia utilizada na disciplina de Engenharia de Software do Curso Técnico em Informática do IFSC Câmpus Chapecó. Ela foi pensada como um estudo de caso, inicialmente para análise de sistemas, e posteriormente para implementação na disciplina de Programação Orientada a Objetos II. Seu sistema foi pensado de modo simples e funcional, permitindo ao escritório manter os registros de funcionários, obras, orçamentos e materiais em estoque. Como resultados, obteve-se a documentação de requisitos, de casos de uso, diagramas de classe e de atividade. Também foi implementado o presente sistema em Java desktop para a disciplina supracitada e, como forma de estudo individual, implementei-o utilizando o .NET Core, Entity Framework e Postgres.

# Docker
docker build --rm -t productive-dev/construtora-viver-sa:latest .

docker run --rm -p 5002:500
2 -p 5001:5001 -e ASPNETCORE_HTTP_PORT=https://+:5001 -e ASPNETCORE_URLS=http://+
:5002 productive-dev/construtora-viver-sa
