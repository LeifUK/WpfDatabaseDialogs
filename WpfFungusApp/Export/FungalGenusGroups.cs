using System.Collections.Generic;

namespace WpfFungusApp.Export
{
    internal class FungalGenusGroups
    {
        internal class Group
        {
            public Group(string name)
            {
                Name = name;
                List = new List<string>();
            }

            public string Name { get; set; }
            public List<string> List { get; set; }
        }

        public List<Group> m_list;

        private List<string> AddGroup(string title)
        {
            Group group = new Group(title);
            m_list.Add(group);
            return group.List;
        }

        public FungalGenusGroups()
        {
            m_list = new List<Group>();

            List<string> list = AddGroup("Slime moulds");
            list.Add("Fuligo");
            list.Add("Lycogala");
            list.Add("Mucilago");
            list.Add("Trichia");
            list.Add("Tubifera");

            list = AddGroup("Cup Fungi");
            list.Add("Aleuria");
            list.Add("Anthracobia");
            list.Add("Bisporella");
            list.Add("Cheilymenia");
            list.Add("Chlorociboria");
            list.Add("Ciboria");
            list.Add("Coprobia");
            list.Add("Disciotis");
            list.Add("Dumontinia");
            list.Add("Geopora");
            list.Add("Helvella acetabulum");
            list.Add("Helvella leucomelaena");
            list.Add("Humaria");
            list.Add("Lachnum");
            list.Add("Melastiza");
            list.Add("Mollisia");
            list.Add("Octospora");
            list.Add("Otidea");
            list.Add("Paxina");
            list.Add("Peziza");
            list.Add("Pulvinula");
            list.Add("Rutstroemia");
            list.Add("Sarcoscypha");
            list.Add("Scutellinia");
            list.Add("Tapesia");
            list.Add("Tazzetta");

            list = AddGroup("Puff Balls and Stalked Puff Balls");
            list.Add("Battarraea");
            list.Add("Bovista");
            list.Add("Calvatia");
            list.Add("Handkea");
            list.Add("Lycoperdon");
            list.Add("Tulostoma");
            list.Add("Vascellum");

            list = AddGroup("Earth Balls and Tubers");
            list.Add("Rhizopogon");
            list.Add("Scleroderma");

            list = AddGroup("Earth Stars");
            list.Add("Geastrum");
            list.Add("Astraeus");

            list = AddGroup("Subterraneum Fungi");
            list.Add("Elaphomyces");
            list.Add("Tuber");

            list = AddGroup("Tooth or Spine Fungi");
            list.Add("Auriscalpium");
            list.Add("Bankera");
            list.Add("Hericium");
            list.Add("Hydnellum");
            list.Add("Hydnum");
            list.Add("Sarcodon");
            list.Add("Phellodon");
            list.Add("Tuber");

            list = AddGroup("Trumpet Fungi");
            list.Add("Cantharellus");
            list.Add("Craterellus");
            list.Add("Pseudocraterellus");

            list = AddGroup("Spindle and Club Fungi");
            list.Add("Clavaria ");
            list.Add("Clavariadelphus");
            list.Add("Clavulinopsis");
            list.Add("Cordyceps");
            list.Add("Macrotyphula");
            list.Add("Mitrula");
            list.Add("Typhula");

            list = AddGroup("Earth Tongues");
            list.Add("Geoglossum");
            list.Add("Microglossum");
            list.Add("Trichoglossum");

            list = AddGroup("Coral Fungi");
            list.Add("Clavaria");
            list.Add("Clavulina");
            list.Add("Clavulinopsis");
            list.Add("Ramaria");
            list.Add("Sparassis");
            list.Add("Thelephora");

            list = AddGroup("Jelly Fungi");
            list.Add("Ascocoryne");
            list.Add("Auricularia");
            list.Add("Bulgaria");
            list.Add("Calocera");
            list.Add("Cudoniella");
            list.Add("Dacrymyces");
            list.Add("Exidia");
            list.Add("Guepinia");
            list.Add("Myxarium");
            list.Add("Neobulgaria");
            list.Add("Pseudohydnum");
            list.Add("Tremella");

            list = AddGroup("Stink Horns");
            list.Add("Dictyophora");
            list.Add("Mutinus");
            list.Add("Phallus");

            list = AddGroup("Birds Nest Fungi");
            list.Add("Dictyophora");
            list.Add("Crucibulum");
            list.Add("Cyathus");
            list.Add("Nidularia");
            list.Add("Sphaerobolus");

            list = AddGroup("Brain Fungi");
            list.Add("Gyromitra");
            list.Add("Mitrophora");
            list.Add("Morchella");
            list.Add("Verpa");

            list = AddGroup("Saddle Fungi");
            list.Add("Cyathipodia");
            list.Add("Helvella crispa");
            list.Add("Helvella lacunosa");
            list.Add("Helvella macropus");

            list = AddGroup("Fungi Growing on Other Fungi");
            list.Add("Asterophora");
            list.Add("Cordyceps canadensis");
            list.Add("Heteromycophaga");
            list.Add("Pseudoboletus");
            list.Add("Volvariella surrecta");

            list = AddGroup("Fungi Growing on Insects");
            list.Add("Cordyceps gracilis");
            list.Add("Cordyceps militaris");

            list = AddGroup("Flask Fungi");
            list.Add("Daldinia");
            list.Add("Diatrype");
            list.Add("Hypoxylon");
            list.Add("Xylaria");

            list = AddGroup("Fleshy Brackets");
            list.Add("Crepidotus");
            list.Add("Hohenbuehelia");
            list.Add("Lentinellus");
            list.Add("Melanotus");
            list.Add("Omphalotus");
            list.Add("Ossicaulis");
            list.Add("Panellus");
            list.Add("Pleurocybella");
            list.Add("Pleurotus");
            list.Add("Schizophyllum");

            list = AddGroup("Succulent Brackets");
            list.Add("Abortiporus");
            list.Add("Aurantiporus");
            list.Add("Grifola");
            list.Add("Fistulina");
            list.Add("Laetiporus");
            list.Add("Meripilus");
            list.Add("Polyporus");
            list.Add("Postia");
            list.Add("Spongipellis");

            list = AddGroup("Hard Brackets");
            list.Add("Antrodia");
            list.Add("Daedalia");
            list.Add("Daedaleopsis");
            list.Add("Datronia");
            list.Add("Fomes");
            list.Add("Fomitopsis");
            list.Add("Ganoderma");
            list.Add("Gloeophyllum");
            list.Add("Heterobasidium");
            list.Add("Hymenochaete rubiginosa");
            list.Add("Inonotus");
            list.Add("Ischnoderma");
            list.Add("Oxyporus");
            list.Add("Phaeolus");
            list.Add("Phellinus igniarius");
            list.Add("Piptoporus");
            list.Add("Pyncnoporus");
            list.Add("Trametes");

            list = AddGroup("Resupinate Fungi");
            list.Add("Antrodia");
            list.Add("Basidioradulum");
            list.Add("Biscogniauxia");
            list.Add("Chondrostereum");
            list.Add("Coniophora");
            list.Add("Cylindrobasidium");
            list.Add("Gloeoporus");
            list.Add("Hymenochaete corrugata");
            list.Add("Peniophora");
            list.Add("Phellinus ferreus");
            list.Add("Phlebia");
            list.Add("Schizopora");
            list.Add("Scytinostroma");
            list.Add("Serpula");
            list.Add("Skeletocutis");
            list.Add("Stereum");
            list.Add("Ustulina");

            list = AddGroup("Boletes");
            list.Add("Aureoboletus");
            list.Add("Boletus");
            list.Add("Gyrodon");
            list.Add("Gyroporus");
            list.Add("Leccinum");
            list.Add("Porphyrellus");
            list.Add("Pseudoboletus");
            list.Add("Strobilomyces");
            list.Add("Suillus");
            list.Add("Tylopilus");

            list = AddGroup("Gilled Mushrooms with White or Pale Spores");
            list.Add("Amanita");
            list.Add("Armillaria");
            list.Add("Arrhenia");
            list.Add("Baeospora");
            list.Add("Camarophyllopsis");
            list.Add("Cantharellula");
            list.Add("Clitocybe");
            list.Add("Collybia");
            list.Add("Cystolepiota");
            list.Add("Flammulina");
            list.Add("Hygrocybe");
            list.Add("Hygrophoropsis");
            list.Add("Laccaria");
            list.Add("Lactarius");
            list.Add("Lepiota");
            list.Add("Leucocoprinus");
            list.Add("Leucopaxillus");
            list.Add("Limacella");
            list.Add("Lyophyllum");
            list.Add("Macrolepiota");
            list.Add("Marasmius");
            list.Add("Melanoleuca");
            list.Add("Mycena");
            list.Add("Omphalotus");
            list.Add("Oudemansiella");
            list.Add("Panellus");
            list.Add("Panus");
            list.Add("Pseudoclitocybe");
            list.Add("Rickenella");
            list.Add("Russula atropurpurea");
            list.Add("Russula emetica");
            list.Add("Russula foetens");
            list.Add("Russula sanguinaria");
            list.Add("Russula nigricans");
            list.Add("Schizophyllum");
            list.Add("Strobilurus");
            list.Add("Tephrocybe");
            list.Add("Tricholoma");
            list.Add("Tricholomopsis");

            list = AddGroup("Gilled Mushrooms with Pink Spores");
            list.Add("Entoloma");
            list.Add("Clitopilus");
            list.Add("Lepista");
            list.Add("Pluteus");
            list.Add("Rhodocybe");
            list.Add("Rhodotus");
            list.Add("Volvariella");

            list = AddGroup("Gilled Mushrooms with Lilac Spores");
            list.Add("Pleurotus");

            list = AddGroup("Gilled Mushrooms with Brownish Spores");
            list.Add("Agrocybe");
            list.Add("Coprinopsis atramentaria");
            list.Add("Crepidotus");
            list.Add("Hypholoma lateritium");
            list.Add("Inocybe");
            list.Add("Naucoria");
            list.Add("Psilocybe");
            list.Add("Stropharia");

            list = AddGroup("Gilled Mushrooms with Rust-Brown or Rust Spores");
            list.Add("Bolbitious");
            list.Add("Conocybe");
            list.Add("Cortinarius");
            list.Add("Galerina");
            list.Add("Gymnopilus");
            list.Add("Phaeolepiota");
            list.Add("Kuehneromyces");
            list.Add("Paxillus");
            list.Add("Pholiota");
            list.Add("Tapinella");

            list = AddGroup("Gilled Mushrooms with Dark Brown Spores");
            list.Add("Coprinellus disseminatus");
            list.Add("Coprinellus domesticus");
            list.Add("Coprinus comatus");
            list.Add("Coprinus micaceus");
            list.Add("Psathyrella multipedata");
            list.Add("Psathyrella piluliformis");
            list.Add("Psilocybe merdaria");

            list = AddGroup("Gilled Mushrooms with Purple-Brown Spores");
            list.Add("Agaricus silvaticus");
            list.Add("Agaricus xanthodermus");
            list.Add("Hypholoma fasciculare");

            list = AddGroup("Gilled Mushrooms with Black, Purple-Black or Brown-Black Spores");
            list.Add("Coprinopsis jonesii");
            list.Add("Coprinopsis lagopus");
            list.Add("Coprinopsis nivea");
            list.Add("Coprinopsis picacea");
            list.Add("Panaeolus");
        }
    }
}
