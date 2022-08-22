using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fourier3D : MonoBehaviour
{
    //Settings
    private int vertCount;
    private float deltaTheta;
    private float inc;
    private int N;
    private float t = 0;
    private bool defaultMode;
    private bool SVGMode;

    //Game Objects
    private GameObject[,] ObjAr_Point;
    private GameObject[,] ObjAr_Circle;
    private GameObject[] ObjAr_Line;
    private GameObject Obj_Image;
    private GameObject Obj_Tracer;

    //CreateObject
    public Material lineMaterial;
    public Material transparentMaterial;
    protected GameObject objectPlaceholder;
    public GameObject Obj_parentLineGrapher;
    public GameObject Obj_parentFourier3D;

    //Grapher
    private Vector3[] dataPoints;
    private int upRange;
    private int downRange;
    private GameObject Obj_Line;
    private Vector3 vector3Placeholder;

    //Fourier function
    public float[,] input; //[axis,input]
    public float[,,] Output; //[axis, (im,re,freq,amp,phase), output]

    //Generating Image
    private Vector3 startingPoint = new Vector3(0, 0, 0);
    private Vector3 currentPoint;

    //SVG Function
    public string str;
    public string[] strArr;
    public string[] stringPlaceholder;

    void Start()
    {
        SVGMode = false;

        str = "451.66473388671875,552.4767456054688],[451.6878356933594,556.2943115234375],[451.75628662109375,560.111328125],[451.8703308105469,563.92724609375],[452.030029296875,567.7415161132812],[452.2361755371094,571.5535888671875],[452.4880676269531,575.3629150390625],[452.78582763671875,579.1688842773438],[453.12945556640625,582.9710693359375],[453.5193786621094,586.7687377929688],[453.95556640625,590.5613403320312],[454.4377746582031,594.348388671875],[454.9659118652344,598.1293334960938],[455.5400085449219,601.9035034179688],[456.1600341796875,605.6704711914062],[456.8262023925781,609.4295043945312],[457.5384216308594,613.1801147460938],[458.2963562011719,616.9217529296875],[459.0999450683594,620.6538696289062],[459.9490966796875,624.3758544921875],[460.84375,628.087158203125],[461.78375244140625,631.7872314453125],[462.76898193359375,635.4755859375],[463.7993469238281,639.1515502929688],[464.87481689453125,642.8145141601562],[465.99505615234375,646.464111328125],[467.1598815917969,650.0996704101562],[468.369140625,653.720703125],[469.62255859375,657.3267211914062],[470.9200134277344,660.9171142578125],[472.26123046875,664.4913940429688],[473.64605712890625,668.0490112304688],[475.07421875,671.5894165039062],[476.5455322265625,675.1121215820312],[478.0596923828125,678.6166381835938],[479.6164245605469,682.1024780273438],[481.2155456542969,685.5690307617188],[482.8567199707031,689.015869140625],[484.5397033691406,692.4425048828125],[486.2642822265625,695.848388671875],[488.03009033203125,699.2330932617188],[489.8367614746094,702.59619140625],[491.68414306640625,705.9370727539062],[493.5718994140625,709.2553100585938],[495.4996337890625,712.5504760742188],[497.46710205078125,715.8220825195312],[499.4739685058594,719.0697021484375],[501.5198669433594,722.2927856445312],[503.60455322265625,725.490966796875],[505.7275695800781,728.6638793945312],[507.8886413574219,731.8109130859375],[510.0874328613281,734.9317626953125],[512.3235778808594,738.0260009765625],[514.5967102050781,741.0930786132812],[516.9065246582031,744.1326293945312],[519.2525634765625,747.1444091796875],[521.6345825195312,750.1277465820312],[524.0521545410156,753.0823364257812],[526.5049133300781,756.0078125],[528.992431640625,758.9037475585938],[531.5144348144531,761.7697143554688],[534.070556640625,764.6053466796875],[536.6602172851562,767.4103393554688],[539.2832641601562,770.18408203125],[541.939208984375,772.9263916015625],[544.6276245117188,775.6369018554688],[547.3482055664062,778.3151245117188],[550.1004638671875,780.9607543945312],[552.8840942382812,783.5734252929688],[555.6985473632812,786.15283203125],[558.5435180664062,788.698486328125],[561.4185791015625,791.2101440429688],[564.3233642578125,793.6873779296875],[567.2573852539062,796.1298828125],[570.2202758789062,798.537353515625],[573.2116088867188,800.9093017578125],[576.2308959960938,803.2456665039062],[579.27783203125,805.5458374023438],[582.351806640625,807.8096923828125],[585.4525146484375,810.0367431640625],[588.5795288085938,812.2267456054688],[591.7323608398438,814.3794555664062],[594.9105224609375,816.4944458007812],[598.1136474609375,818.5714721679688],[601.34130859375,820.6102905273438],[604.593017578125,822.6105346679688],[607.8682861328125,824.5718994140625],[611.1666870117188,826.4940795898438],[614.4877319335938,828.3768920898438],[617.8309936523438,830.2200317382812],[621.1959838867188,832.0230102539062],[624.5821533203125,833.7859497070312],[627.9891967773438,835.5083618164062],[631.4164428710938,837.1900634765625],[634.8635864257812,838.8307495117188],[638.3299560546875,840.4302368164062],[641.8151245117188,841.9884033203125],[645.3186645507812,843.5047607421875],[648.8401489257812,844.9789428710938],[652.3789672851562,846.4111938476562],[655.9346313476562,847.8009643554688],[659.5066528320312,849.148193359375],[663.094482421875,850.4527587890625],[666.6976318359375,851.7144165039062],[670.3155517578125,852.9330444335938],[673.94775390625,854.1083374023438],[677.5936889648438,855.240234375],[681.2528686523438,856.32861328125],[684.9248046875,857.3731689453125],[688.6089477539062,858.3738403320312],[692.3048706054688,859.3303833007812],[696.0117797851562,860.242919921875],[699.7293090820312,861.111328125],[703.4569091796875,861.935546875],[707.1940307617188,862.7153930664062],[710.9402465820312,863.4505004882812],[714.6949462890625,864.1409912109375],[718.45751953125,864.7869873046875],[722.2274169921875,865.3885498046875],[726.004150390625,865.9454956054688],[729.7872924804688,866.4575805664062],[733.5762329101562,866.9246826171875],[737.370361328125,867.3473510742188],[741.169189453125,867.7255249023438],[744.9722900390625,868.0588989257812],[748.779052734375,868.3473510742188],[752.5888671875,868.5912475585938],[756.4012451171875,868.7908935546875],[760.2157592773438,868.9459228515625],[764.0317993164062,869.0563354492188],[767.848876953125,869.1226806640625],[771.6664428710938,869.1450805664062],[775.4840087890625,869.1226806640625],[779.3010864257812,869.0563354492188],[783.1170654296875,868.9459228515625],[786.9315795898438,868.7908935546875],[790.7440185546875,868.5912475585938],[794.5537719726562,868.3473510742188],[798.360595703125,868.0588989257812],[802.16357421875,867.7255249023438],[805.9624633789062,867.3473510742188],[809.756591796875,866.9246215820312],[813.5455932617188,866.45751953125],[817.3286743164062,865.9454345703125],[821.10546875,865.3884887695312],[824.8753662109375,864.7869873046875],[828.6380004882812,864.1409301757812],[832.3926391601562,863.450439453125],[836.1388549804688,862.71533203125],[839.8759155273438,861.9354858398438],[843.6036376953125,861.111328125],[847.3211059570312,860.242919921875],[851.0281372070312,859.330322265625],[854.7239379882812,858.373779296875],[858.4080810546875,857.3731689453125],[862.0796508789062,856.3272705078125],[865.7388305664062,855.239013671875],[869.3851928710938,854.1082763671875],[873.0173950195312,852.9329833984375],[876.635009765625,851.7136840820312],[880.2382202148438,850.4521484375],[883.8262329101562,849.1481323242188],[887.3980712890625,847.8005981445312],[890.9537963867188,846.4109497070312],[894.49267578125,844.9788208007812],[898.01416015625,843.5046997070312],[901.5177612304688,841.9883422851562],[905.0028686523438,840.4299926757812],[908.4691772460938,838.8304443359375],[911.916259765625,837.189697265625],[915.3434448242188,835.5079345703125],[918.7504272460938,833.785400390625],[922.1365966796875,832.0223999023438],[925.5015869140625,830.2193603515625],[928.8447875976562,828.3761596679688],[932.1658325195312,826.4933471679688],[935.4641723632812,824.571044921875],[938.7393798828125,822.609619140625],[941.9910278320312,820.6094360351562],[945.2186279296875,818.570556640625],[948.4216918945312,816.4935913085938],[951.5999145507812,814.3784790039062],[954.7526245117188,812.225830078125],[957.879638671875,810.0357666015625],[960.9803466796875,807.8087158203125],[964.0543823242188,805.5448608398438],[967.1011962890625,803.2447509765625],[970.1204833984375,800.908447265625],[973.1116943359375,798.5364379882812],[976.0745849609375,796.1290283203125],[979.0086059570312,793.6865234375],[981.913330078125,791.2092895507812],[984.788330078125,788.6976318359375],[987.6333618164062,786.1519775390625],[990.44775390625,783.5726318359375],[993.2313232421875,780.9599609375],[995.9835815429688,778.3143920898438],[998.7041015625,775.6361694335938],[1001.3925170898438,772.92578125],[1004.0485229492188,770.183349609375],[1006.6715087890625,767.4096069335938],[1009.26123046875,764.6046752929688],[1011.8171997070312,761.7691040039062],[1014.3392944335938,758.9031372070312],[1016.8267822265625,756.0072021484375],[1019.2794799804688,753.0816650390625],[1021.697021484375,750.1270751953125],[1024.0790405273438,747.1437377929688],[1026.4251098632812,744.132080078125],[1028.7349243164062,741.0924682617188],[1031.0079956054688,738.025390625],[1033.2442016601562,734.93115234375],[1035.4429321289062,731.810302734375],[1037.60400390625,728.663330078125],[1039.72705078125,725.490478515625],[1041.8117065429688,722.292236328125],[1043.8575439453125,719.0691528320312],[1045.8642578125,715.821533203125],[1047.8330078125,712.5506591796875],[1049.7593994140625,709.2548217773438],[1051.6473388671875,705.9365844726562],[1053.49462890625,702.5957641601562],[1055.30126953125,699.232666015625],[1057.0672607421875,695.8480834960938],[1058.791748046875,692.4421997070312],[1060.4747314453125,689.0155639648438],[1062.1170654296875,685.5692138671875],[1063.7161865234375,682.1027221679688],[1065.27197265625,678.616455078125],[1066.7862548828125,675.1119995117188],[1068.257568359375,671.5892944335938],[1069.6866455078125,668.0492553710938],[1071.071533203125,664.4915771484375],[1072.4127197265625,660.9173583984375],[1073.7100830078125,657.3269653320312],[1074.9635009765625,653.7210083007812],[1076.1728515625,650.0999145507812],[1077.3375244140625,646.4644165039062],[1078.457763671875,642.8147583007812],[1079.5333251953125,639.1517944335938],[1080.563720703125,635.4757690429688],[1081.5489501953125,631.7875366210938],[1082.4888916015625,628.0873413085938],[1083.3836669921875,624.3760986328125],[1084.23291015625,620.654052734375],[1085.0364990234375,616.9219970703125],[1085.7943115234375,613.1802978515625],[1086.5064697265625,609.4296875],[1087.1727294921875,605.670654296875],[1087.792724609375,601.9037475585938],[1088.366943359375,598.1295166015625],[1088.8951416015625,594.3486328125],[1089.377197265625,590.5615234375],[1089.8133544921875,586.7689208984375],[1090.2032470703125,582.97119140625],[1090.546875,579.1690673828125],[1090.8446044921875,575.363037109375],[1091.096435546875,571.5537719726562],[1091.302734375,567.7416381835938],[1091.46240234375,563.9274291992188],[1091.576416015625,560.1115112304688],[1091.64501953125,556.29443359375],[1091.6680908203125,552.4768676757812],[1091.64501953125,548.6593017578125],[1091.576416015625,544.84228515625],[1091.46240234375,541.0263061523438],[1091.302734375,537.2120971679688],[1091.096435546875,533.4000244140625],[1090.8446044921875,529.5907287597656],[1090.546875,525.7846984863281],[1090.2032470703125,521.9825439453125],[1089.8133544921875,518.1849060058594],[1089.377197265625,514.3922119140625],[1088.8951416015625,510.60516357421875],[1088.3670654296875,506.8243103027344],[1087.792724609375,503.0500793457031],[1087.1728515625,499.28314208984375],[1086.5064697265625,495.5240173339844],[1085.7943115234375,491.77349853515625],[1085.0364990234375,488.0318298339844],[1084.23291015625,484.29962158203125],[1083.3837890625,480.57769775390625],[1082.489013671875,476.86651611328125],[1081.549072265625,473.16644287109375],[1080.5638427734375,469.47796630859375],[1079.533447265625,465.80206298828125],[1078.4578857421875,462.13909912109375],[1077.337646484375,458.489501953125],[1076.1729736328125,454.8538818359375],[1074.963623046875,451.23291015625],[1073.7103271484375,447.6268615722656],[1072.412841796875,444.0365295410156],[1071.071533203125,440.4621276855469],[1069.686767578125,436.904541015625],[1068.258544921875,433.36419677734375],[1066.787353515625,429.841552734375],[1065.2730712890625,426.33697509765625],[1063.71630859375,422.85113525390625],[1062.1171875,419.3846130371094],[1060.47607421875,415.9377746582031],[1058.7930908203125,412.51104736328125],[1057.068603515625,409.1051330566406],[1055.302734375,405.7205810546875],[1053.49609375,402.3575134277344],[1051.6488037109375,399.0164794921875],[1049.7608642578125,395.6983642578125],[1047.833251953125,392.4031982421875],[1045.8656005859375,389.13153076171875],[1043.8589477539062,385.88385009765625],[1041.8131103515625,382.66082763671875],[1039.7283935546875,379.4625244140625],[1037.6053466796875,376.2897033691406],[1035.4441528320312,373.1426086425781],[1033.2454223632812,370.02178955078125],[1031.00927734375,366.9276123046875],[1028.7361450195312,363.8604431152344],[1026.4263305664062,360.82086181640625],[1024.0802612304688,357.8091735839844],[1021.6983032226562,354.825927734375],[1019.2806396484375,351.8711853027344],[1016.8279418945312,348.9457092285156],[1014.3404541015625,346.04986572265625],[1011.8184204101562,343.183837890625],[1009.2622680664062,340.3481750488281],[1006.6726684570312,337.5433044433594],[1004.0496215820312,334.7694091796875],[1001.3936767578125,332.02716064453125],[998.7052612304688,329.3166809082031],[995.984619140625,326.638427734375],[993.2323608398438,323.9928283691406],[990.4488525390625,321.3802185058594],[987.6343383789062,318.8008117675781],[984.7893676757812,316.2552185058594],[981.914306640625,313.7435607910156],[979.009521484375,311.2662658691406],[976.0755004882812,308.82373046875],[973.1126708984375,306.4163513183594],[970.121337890625,304.0443115234375],[967.1021118164062,301.70806884765625],[964.0550537109375,299.4077453613281],[960.9810791015625,297.1439514160156],[957.88037109375,294.9169616699219],[954.75341796875,292.7268981933594],[951.6005249023438,290.57421875],[948.4223022460938,288.4591369628906],[945.21923828125,286.3822021484375],[941.9915161132812,284.3434143066406],[938.73974609375,282.3431091308594],[935.4645385742188,280.3818359375],[932.1661376953125,278.4595947265625],[928.8450927734375,276.5768127441406],[925.5018310546875,274.7336730957031],[922.1369018554688,272.93055725097656],[918.7507934570312,271.1676483154297],[915.3436889648438,269.44517517089844],[911.9164428710938,267.76348876953125],[908.4693603515625,266.1227722167969],[905.0029907226562,264.5233154296875],[901.5176391601562,262.96519470214844],[898.0140991210938,261.4488067626953],[894.4926147460938,259.97454833984375],[890.9539184570312,258.54246520996094],[887.3982543945312,257.1526184082031],[883.8262939453125,255.8053741455078],[880.2383422851562,254.50079345703125],[876.6353149414062,253.23915100097656],[873.0172729492188,252.02053833007812],[869.3850708007812,250.8451690673828],[865.7391357421875,249.7132568359375],[862.080078125,248.6248779296875],[858.4078979492188,247.58033752441406],[854.723876953125,246.57980346679688],[851.028076171875,245.6232147216797],[847.3211669921875,244.71066284179688],[843.6034545898438,243.84219360351562],[839.8759155273438,243.01803588867188],[836.1387329101562,242.23812866210938],[832.392578125,241.50306701660156],[828.6378784179688,240.81253051757812],[824.8753051757812,240.16650390625],[821.10546875,239.56500244140625],[817.32861328125,239.00802612304688],[813.5454711914062,238.4959716796875],[809.7565307617188,238.02877807617188],[805.9623413085938,237.6061553955078],[802.16357421875,237.22805786132812],[798.3604736328125,236.8945770263672],[794.5537109375,236.60621643066406],[790.743896484375,236.36224365234375],[786.931640625,236.16258239746094],[783.1170043945312,236.0074920654297],[779.301025390625,235.8971405029297],[775.4839477539062,235.830810546875],[771.6663818359375,235.8083953857422],[767.8487548828125,235.830810546875],[764.0316772460938,235.8971405029297],[760.2156982421875,236.0074920654297],[756.4011840820312,236.16258239746094],[752.5887451171875,236.3622589111328],[748.7789916992188,236.60623168945312],[744.9722900390625,236.8945770263672],[741.169189453125,237.22805786132812],[737.3702392578125,237.60617065429688],[733.5762329101562,238.02877807617188],[729.7872314453125,238.4959716796875],[726.0039672851562,239.00804138183594],[722.227294921875,239.56500244140625],[718.4573364257812,240.16653442382812],[714.69482421875,240.8125457763672],[710.9400024414062,241.5030975341797],[707.1939697265625,242.23812866210938],[703.4568481445312,243.01803588867188],[699.729248046875,243.8422088623047],[696.0116577148438,244.7106475830078],[692.3046875,245.6232147216797],[688.6088256835938,246.57981872558594],[684.9247436523438,247.5803680419922],[681.252685546875,248.6248779296875],[677.5935668945312,249.71327209472656],[673.9476318359375,250.84518432617188],[670.3154296875,252.0205535888672],[666.6973876953125,253.23916625976562],[663.0942993164062,254.50082397460938],[659.506591796875,255.8053436279297],[655.9345703125,257.15260314941406],[652.37890625,258.5424499511719],[648.8400268554688,259.974609375],[645.318603515625,261.4488525390625],[641.8150024414062,262.96527099609375],[638.3297119140625,264.52333068847656],[634.8633422851562,266.12278747558594],[631.4163208007812,267.7634735107422],[627.989013671875,269.4451904296875],[624.58203125,271.1676330566406],[621.1958618164062,272.93055725097656],[617.8309326171875,274.73365783691406],[614.48779296875,276.5767517089844],[611.1665649414062,278.4596252441406],[607.8682861328125,280.3818054199219],[604.593017578125,282.3431091308594],[601.3412475585938,284.3433837890625],[598.1135864257812,286.38214111328125],[594.9104614257812,288.45916748046875],[591.7323608398438,290.57415771484375],[588.579345703125,292.72686767578125],[585.452392578125,294.91693115234375],[582.3516845703125,297.1439208984375],[579.2777709960938,299.4076843261719],[576.2308349609375,301.7079162597656],[573.2115478515625,304.0442199707031],[570.2202758789062,306.4162292480469],[567.2573852539062,308.8236389160156],[564.3233032226562,311.26617431640625],[561.4185791015625,313.7434997558594],[558.5435180664062,316.255126953125],[555.6985473632812,318.80072021484375],[552.8840942382812,321.38006591796875],[550.1004028320312,323.9927978515625],[547.34814453125,326.638427734375],[544.627685546875,329.3165283203125],[541.9391479492188,332.027099609375],[539.283203125,334.7693786621094],[536.6602172851562,337.54315185546875],[534.070556640625,340.34808349609375],[531.5144348144531,343.18377685546875],[528.992431640625,346.04974365234375],[526.5048828125,348.9456787109375],[524.0521240234375,351.8711242675781],[521.6345520019531,354.82574462890625],[519.2525024414062,357.8091125488281],[516.906494140625,360.82073974609375],[514.5967102050781,363.8603515625],[512.3235473632812,366.927490234375],[510.08740234375,370.0216979980469],[507.888671875,373.1424560546875],[505.72760009765625,376.28948974609375],[503.6044921875,379.4624938964844],[501.5198974609375,382.6606140136719],[499.4739990234375,385.8836975097656],[497.4671630859375,389.1313171386719],[495.49969482421875,392.4029235839844],[493.5719299316406,395.69805908203125],[491.68414306640625,399.0163269042969],[489.83673095703125,402.3573303222656],[488.0299987792969,405.7203674316406],[486.2642822265625,409.1051025390625],[484.5397033691406,412.5109558105469],[482.8567199707031,415.9375915527344],[481.21551513671875,419.38446044921875],[479.6164245605469,422.85101318359375],[478.0597229003906,426.3367614746094],[476.5455017089844,429.84136962890625],[475.0742492675781,433.364013671875],[473.64605712890625,436.90447998046875],[472.26123046875,440.4620666503906],[470.9199523925781,444.0364685058594],[469.62255859375,447.6267395019531],[468.369140625,451.23272705078125],[467.159912109375,454.8536682128906],[465.99505615234375,458.4893798828125],[464.87481689453125,462.13897705078125],[463.7993469238281,465.8019714355469],[462.76898193359375,469.4778747558594],[461.78375244140625,473.1662292480469],[460.84375,476.8663330078125],[459.9491271972656,480.5775146484375],[459.0999755859375,484.29949951171875],[458.29632568359375,488.0317687988281],[457.5384216308594,491.7733154296875],[456.82623291015625,495.52386474609375],[456.1600646972656,499.28302001953125],[455.5400085449219,503.04998779296875],[454.9659118652344,506.82415771484375],[454.4377746582031,510.60498046875],[453.95556640625,514.3920593261719],[453.5193176269531,518.1848449707031],[453.12945556640625,521.982421875],[452.7857666015625,525.7846069335938],[452.488037109375,529.5904541015625],[452.2361145019531,533.39990234375],[452.0299987792969,537.2119750976562],[451.8703308105469,541.0261840820312],[451.75628662109375,544.842041015625],[451.68780517578125,548.6592407226562]]";
        strArr = str.Split('[');


        Settings();
        SetArrayLengths();

        if (SVGMode)
        {
            GenerateSVGDataPoints();
        }
        else
        {
            GenerateEquationDataPoints();
        }

        for (int i = 0; i < N; i++)
        {
            RotatePointAroundPivot(dataPoints[i], new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f));
            dataPoints[i] = vector3Placeholder;
        }


        CalcStartingPoint();
        //RenderGraph();
        ConvertDataPointsToArray();
        GenerateDrawings();
        LoadTracer();
        FourierTransform();
        StartCoroutine(LoadImages());

        if (defaultMode)
        {
            StartCoroutine(UpdateTime());
        }
    }

    void Update()
    {
        UpdateDrawings();
        TraceImage();

        if (defaultMode != true)
        {
            InstantUpdateTime();
        }
    }

    //SETTINGS
    void Settings()
    {
        //for equation only
        upRange = 50;
        downRange = -50;

        //general
        vertCount = 40;
        inc = 0.01f;
        defaultMode = true;

        if (SVGMode)
        {
            N = strArr.Length;
        }
        else
        { N = upRange - downRange; }

        Obj_parentFourier3D = GameObject.Find("Fourier3D");
        Obj_parentLineGrapher = GameObject.Find("LineGraph");
        lineMaterial = (Material)Resources.Load("LineMat", typeof(Material));
        transparentMaterial = (Material)Resources.Load("TransparentMat", typeof(Material));

    }

    void SetArrayLengths()
    {
        input = new float[3, N];
        Output = new float[3, 5, N];

        ObjAr_Circle = new GameObject[3, N];
        ObjAr_Point = new GameObject[3, N];
        ObjAr_Line = new GameObject[3];

        dataPoints = new Vector3[N];
        deltaTheta = (2 * Mathf.PI) / vertCount;

  
    }

    void CalcStartingPoint()
    {
        for (int i = 0; i < 3; i++)
        {
            float a = 0;
            float freq = Output[i, 2, 0];
            float radius = Output[i, 3, 0];
            float phase = Output[i, 4, 0];

            //Calc for a
            a += radius * Mathf.Cos((freq * t) + phase);

            //Conditions to assign position
            if (i == 0)
            {
                startingPoint.x = a;
            }

            else if (i == 1)
            {
                startingPoint.y = a;
            }

            else if (i == 2)
            {
                startingPoint.z = a;
            }

        }

    }

    //LINEGRAPH
    void GenerateEquationDataPoints()
    {

        for (int t = 0; t < N; t++)
        {
            int tOffset = t + downRange;
            float x = 30 * Mathf.Sign(0.5f * tOffset-100);
            float y = tOffset;
            float z = 0;

            dataPoints[t] = new Vector3(x, y, z);

        }
    }

    void GenerateSVGDataPoints()
    {

        for (int i = 0; i < strArr.Length; i++)
        {
            int length = strArr[i].Length - 2;

            strArr[i] = strArr[i].Substring(0, length);

            stringPlaceholder = strArr[i].Split(',');

            float x = float.Parse(stringPlaceholder[0]);
            float y = float.Parse(stringPlaceholder[1]);

            dataPoints[i] = new Vector3(x, y, 0f);


        }

    }

    void RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = point - pivot;
        dir = Quaternion.Euler(angles) * dir;
        Vector3 newPoint = dir + pivot;
        vector3Placeholder = newPoint;
    }


    void RenderGraph()
    {
        CreateObject("line", Color.white, Obj_parentLineGrapher);
        Obj_Line = objectPlaceholder;
        LineRenderer newRenderer = Obj_Line.GetComponent<LineRenderer>();
        newRenderer.material = transparentMaterial;
        newRenderer.material.color = new Color(1f, 1f, 1f, 0.1f);
        newRenderer.material.SetColor("_EmissionColor", new Color(0f, 0f, 0f, 1f));

        for (int t = 0; t < N - 2; t++)
        {
            newRenderer.positionCount++;
        }

        for (int t = 0; t < N; t++)
        {

            newRenderer.SetPosition(t, dataPoints[t]);

        }
    }

    void ConvertDataPointsToArray()
    {
        for (int i = 0; i < N; i++)
        {
            input[0, i] = dataPoints[i].x;
            input[1, i] = dataPoints[i].y;
            input[2, i] = dataPoints[i].z;

        }

    }


    //SET UP FOURIER
    void TraceImage()
    {
        GetCurrentPoint();
        Obj_Tracer.transform.position = currentPoint;

    }

    void GetCurrentPoint()
    {
        float x = ObjAr_Point[0, N - 1].transform.position.x;
        float y = ObjAr_Point[1, N - 1].transform.position.y;
        float z = ObjAr_Point[2, N - 1].transform.position.z;

        currentPoint = new Vector3(x, y, z);

    }

    void LoadTracer()
    {

        CreateObject("Tracer", Color.blue, Obj_parentFourier3D);
        Obj_Tracer = objectPlaceholder;
    }

    void LoadImage()
    {

        for (int i = 0; i < N; i++)
        {
            CreateObject("Image", Color.red, Obj_parentFourier3D);
            Vector3 pos = new Vector3(input[0, i], input[1, i], input[2, i]);
            objectPlaceholder.transform.position = pos;
        }
    }

    void GenerateDrawings()
    {
        //COLORS
        Color lineColor = Color.yellow;
        Color imageColor = Color.white;
        Color circleColor = Color.magenta;
        Color pointColor = Color.white;

        //LINE
        CreateObject("LineX", lineColor, Obj_parentFourier3D);
        ObjAr_Line[0] = objectPlaceholder;

        CreateObject("LineY", lineColor, Obj_parentFourier3D);
        ObjAr_Line[1] = objectPlaceholder;

        CreateObject("LineZ", lineColor, Obj_parentFourier3D);
        ObjAr_Line[2] = objectPlaceholder;

        for (int i = 0; i < 3; i++)
        {
            GameObject newGameObject = ObjAr_Line[i];
            LineRenderer newRenderer = newGameObject.GetComponent<LineRenderer>();

            for (int j = 0; j < N + 2; j++)
            {
                newRenderer.positionCount++;
            }
        }

        //IMAGE
        CreateObject("Image", imageColor, Obj_parentFourier3D);
        Obj_Image = objectPlaceholder;
        Obj_Image.transform.position = startingPoint;

        //POINT & CIRCLE
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < N; j++)
            {
                CreateObject("Point " + i + "," + j, pointColor, Obj_parentFourier3D);
                ObjAr_Point[i, j] = objectPlaceholder;

                CreateObject("Circle " + i + "," + j, circleColor, Obj_parentFourier3D);
                ObjAr_Circle[i, j] = objectPlaceholder;

                LineRenderer circleRenderer = ObjAr_Circle[i, j].GetComponent<LineRenderer>();

                for (int k = 0; k < vertCount - 1; k++)
                {
                    circleRenderer.positionCount++;
                }
            }

        }

    }

    void FourierTransform()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int k = 0; k < N; k++)
            {
                float re = 0;
                float im = 0;

                for (int n = 0; n < N; n++)
                {
                    float phi = (2 * Mathf.PI * k * n) / N;
                    re += input[i, n] * Mathf.Cos(phi);
                    im -= input[i, n] * Mathf.Sin(phi);
                }
                re = re / N;
                im = im / N;

                float freq = k;
                float amp = Mathf.Sqrt((re * re) + (im * im));
                float phase = Mathf.Atan(im / re);

                Output[i, 0, k] = re;
                Output[i, 1, k] = im;
                Output[i, 2, k] = freq;
                Output[i, 3, k] = amp;
                Output[i, 4, k] = phase;
            }

        }

    }

    //UPDATE FUNCTIONS
    void UpdateDrawings()
    {
        for (int i = 0; i < 3; i++)
        {
            float a = 0;
            float b = 0;
            Vector3 pos = new Vector3(0f, 0f, 0f);

            float locala;
            float localb;
            Vector3 localPos = new Vector3(0f, 0f, 0f);

            Vector3 prevPos = new Vector3(0f, 0f, 0f);
            Vector3 circleOffset = new Vector3(0f, 0f, 0f);


            //LINE (Last 2 points)
            LineRenderer lineRenderer = ObjAr_Line[i].GetComponent<LineRenderer>();
            Vector3 tracerPos1 = new Vector3(0f, 0f, 0f);
            Vector3 tracerPos2 = new Vector3(0f, 0f, 0f);
            Vector3 tracerPos3 = Obj_Tracer.transform.position;

            if (i == 0)
            {
                tracerPos1 = new Vector3(tracerPos3.x, 0f, 0f);
                tracerPos2 = new Vector3(tracerPos3.x, 0f, tracerPos3.z);
            }

            else if (i == 1)
            {
                tracerPos1 = new Vector3(0f, tracerPos3.y, 0f);
                tracerPos2 = new Vector3(tracerPos3.x, tracerPos3.y, 0f);
            }

            else if (i == 2)
            {
                tracerPos1 = new Vector3(0f, 0f, tracerPos3.z);
                tracerPos2 = new Vector3(0f, tracerPos3.y, tracerPos3.z);
            }

            lineRenderer.SetPosition(N + 1, tracerPos1);
            lineRenderer.SetPosition(N + 2, tracerPos2);
            lineRenderer.SetPosition(N + 3, tracerPos3);


            for (int j = 0; j < N; j++)
            {
                //Define Mathematical Variables
                float theta = 0;
                float freq = Output[i, 2, j];
                float radius = Output[i, 3, j];
                float phase = Output[i, 4, j];

                prevPos = pos;

                //Generate Position
                locala = radius * Mathf.Cos((freq * t) + phase);
                localb = radius * Mathf.Sin((freq * t) + phase);

                a += locala;
                b -= localb;

                //Conditions to assign position
                if (i == 0)
                {
                    pos = new Vector3(a, b, 0);
                    circleOffset = new Vector3(radius, 0, 0);
                }

                else if (i == 1)
                {
                    pos = new Vector3(0, a, b);
                    circleOffset = new Vector3(0, radius, 0);
                }

                else if (i == 2)
                {
                    pos = new Vector3(b, 0, a);
                    circleOffset = new Vector3(0, 0, radius);
                }

                //LINE & POINT
                ObjAr_Point[i, j].transform.position = pos;
                lineRenderer.SetPosition(j + 1, pos);

                //CIRCLE
                ObjAr_Circle[i, j].transform.position = prevPos + circleOffset;
                LineRenderer circleRenderer = ObjAr_Circle[i, j].GetComponent<LineRenderer>();

                for (int k = 0; k < vertCount + 1; k++)
                {
                    locala = radius * Mathf.Cos(theta) - radius;
                    localb = radius * Mathf.Sin(theta);

                    if (i == 0)
                    {
                        localPos = new Vector3(locala, localb, 0);
                    }

                    else if (i == 1)
                    {
                        localPos = new Vector3(0, locala, localb);
                    }

                    else if (i == 2)
                    {
                        localPos = new Vector3(localb, 0, locala);
                    }

                    circleRenderer.SetPosition(k, localPos);

                    theta += deltaTheta;
                }

            }

        }

    }

    void InstantUpdateTime()
    {
        t += inc;
    }

    //OTHERS
    IEnumerator UpdateTime()
    {
        yield return new WaitForSeconds(inc);

        float dt = Mathf.PI * 2 / N;
        t += dt;

        if (t > Mathf.PI * 2)
        {
            t = 0;

            LineRenderer lr = Obj_Image.GetComponent<LineRenderer>();
            lr.positionCount = 1;

        }

        StartCoroutine(UpdateTime());
    }

    IEnumerator LoadImages()
    {
        yield return new WaitForSeconds(inc);

        LineRenderer imageRenderer = Obj_Image.GetComponent<LineRenderer>();

        //generate point
        Vector3 pos = currentPoint - startingPoint;

        imageRenderer.SetPosition(imageRenderer.positionCount - 1, pos);
        imageRenderer.positionCount++;

        StartCoroutine(LoadImages());

    }

    void CreateObject(string name, Color color, GameObject parent)
    {
        GameObject newObject = new GameObject(name);
        LineRenderer objRenderer = newObject.AddComponent<LineRenderer>();

        objRenderer.material = lineMaterial;
        objRenderer.numCornerVertices = 30;
        objRenderer.numCapVertices = 30;
        objRenderer.useWorldSpace = false;
        objRenderer.material.SetColor("_Color", color);

        Transform targetTransform = parent.transform;
        newObject.transform.SetParent(targetTransform);
        objectPlaceholder = newObject;

    }

}

